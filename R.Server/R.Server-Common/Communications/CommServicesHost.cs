#pragma warning disable 1692

using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;

using R.Server.ClientCommon;
using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	internal class CommServicesHost : ServiceConsumer, ICommServicesHost, IDisposable
	{
		private const string LogSource = "CommServicesHost";

		private readonly ServiceHost[] _hosts = new ServiceHost[0];
		private readonly LogHelper _logHelper;

		private readonly Dictionary<ServiceHost, CommServiceInfo> _infos =
			new Dictionary<ServiceHost, CommServiceInfo>();

		private readonly ServiceManager _serviceManager;
		private readonly Uri[] _endpointUris;
		private readonly IServerEndpointConfig[] _endpointConfigs;
		private readonly CommHelper _commHelper;
		private readonly string[] _commPolicies;

		[ExpectService(Required = false)]
		private IRegKeyedElementsService<string, CommServiceInfo> _commServicesSvc;

		[ExpectService]
		private ISecurityPolicyManager _policyMgr;

		public CommServicesHost(IServiceProvider provider, IServerEndpointConfig[] endpointConfigs,
			string[] commPolicies) : base(provider)
		{
			if (endpointConfigs == null)
				throw new ArgumentNullException("endpointConfigs");
			if (endpointConfigs.Length == 0)
				throw new ArgumentException("At least one endpoint must be defined", "endpointConfigs");
			_endpointConfigs = endpointConfigs;
			_commPolicies = commPolicies;

			_serviceManager = new ServiceManager(provider);
			_serviceManager.Publish<IInstanceProvider>(new CommSvcInstanceProvider(_serviceManager));
			_logHelper = new LogHelper(provider, LogSource);
			_commHelper = new CommHelper(provider);

			if (_commServicesSvc != null)
			{
				var uris = new List<Uri>();
				foreach (var epCfg in endpointConfigs)
					uris.Add(
						_commHelper.CreateEndpointUri(epCfg.Transport, "localhost", epCfg.Port, epCfg.Path));
				_endpointUris = uris.ToArray();

				var wcfAuthMgr = new RServerWcfAuthorizationMgr(_serviceManager, CheckCommAccess);
				foreach (var policy in commPolicies)
					_logHelper.LogInfo("Use '" + policy + "' security policy.",
						LogEventImpotance.Unimportant);
				var validator = new FakeValidator();
				_hosts = _commServicesSvc.GetRegisteredElements().ConvertAll(
					info =>
						{
							// Create WCF service host
							var svcUris = _endpointUris.ConvertAll(uri => new Uri(uri, info.Name));
							var host = new ServiceHost(info.Type, svcUris);
							host.Extensions.Add(new ServiceProviderExtension(_serviceManager));

							// Create host endpoints
							for (var i = 0; i < svcUris.Length; i++)
								host.AddServiceEndpoint(info.ContractType,
									_commHelper.CreateBinding(_endpointConfigs[i].Transport), svcUris[i]);

							// Attach custom authentication
							host.Credentials.UserNameAuthentication.CacheLogonTokens = true;
							host.Credentials.UserNameAuthentication.UserNamePasswordValidationMode =
								UserNamePasswordValidationMode.Custom;
							host.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = validator;

							// Attach custom authorization
							host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
							host.Authorization.ServiceAuthorizationManager = wcfAuthMgr;

							_infos.Add(host, info);
							return host;
						});
			}
			else
				_logHelper.LogWarning("There are no communication services registered.");

			_logHelper.LogInfo(
				"Communication services host initialized with " + _hosts.Length + " service(s)",
				LogEventImpotance.Unimportant);
		}

		public void Run()
		{
			for (var i = 0; i < _endpointUris.Length; i++)
				_logHelper.LogInfo(string.Format("Listen endpoint '{0}' at address '{1}'",
					_endpointConfigs[i].Name, _endpointUris[i]),
					LogEventImpotance.Unimportant);
			foreach (var host in _hosts)
			{
				host.Open();
				_logHelper.LogInfo("Service '" + _infos[host].Name + "' activated");
			}
		}

		private void Stop()
		{
			foreach (var host in _hosts)
				host.Close();
		}

		#region Authorization
		private bool CheckCommAccess(string serviceName, Type serviceType, string methodName,
			IRServerPrincipal principal)
		{
			if (_commPolicies.Length > 0)
			{
				var ctx = new CommPolicyContext(serviceName, serviceType, methodName);
				return _policyMgr.IsPermitted(_serviceManager, principal, ctx, _commPolicies);
			}
			return true;
		}
		#endregion

		#region IDisposable Members
		///<summary>
		///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		///</summary>
		///<filterpriority>2</filterpriority>
		public void Dispose()
		{
			Stop();
		}
		#endregion

		#region ICommServicesHost Members
		public bool IsServiceRegistered(string svcName)
		{
			return _commServicesSvc.ContainsElement(svcName);
		}
		#endregion

		#region FakeValidator class
		private class FakeValidator : UserNamePasswordValidator
		{
			public override void Validate(string userName, string password)
			{
			}
		}
		#endregion
	}
}
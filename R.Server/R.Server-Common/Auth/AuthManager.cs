#pragma warning disable 1692

using System;
using System.Collections.Generic;

using R.Server.ServerModel;

using Rsdn.SmartApp;
using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	[Service(typeof (IAuthManager))]
	public class AuthManager : ServiceConsumer, IAuthManager, IDisposable
	{
		public const string LogSource = "AuthManager";

		private readonly Dictionary<string, IAuthenticationProvider> _authenticationProviders =
			new Dictionary<string, IAuthenticationProvider>();

		private readonly LogHelper _logHelper;

		private readonly IAuthorizationProvider[] _authorizationProviders =
			new IAuthorizationProvider[0];

#pragma warning disable ConvertToConstant
		[ExpectService]
		private IConfigService _cfgSvc = null;

		[ExpectService(Required = false)]
		private IRegKeyedElementsService<string, AuthenticationProviderInfo> _authenSvc = null;

		[ExpectService(Required = false)]
		private IRegKeyedElementsService<string, AuthorizationProviderInfo> _authorSvc = null;
#pragma warning restore ConvertToConstant

		public AuthManager(IServiceProvider provider) : base(provider)
		{
			if (provider == null)
				throw new ArgumentNullException("provider");

			var cfg = _cfgSvc.GetSection<IRServerConfig>();

			_logHelper = new LogHelper(provider, LogSource);

			var authenProviders = cfg.AuthenticationProviders;
			if (authenProviders.Length > 0)
			{
				if (_authenSvc == null)
					throw new ApplicationException("No authentication providers registered.");
				foreach (var provName in authenProviders)
				{
					var info = _authenSvc.GetElement(provName);
					if (info == null)
						throw new ArgumentException("Provider '" + provName + "' not registered.");
					_authenticationProviders.Add(info.Name,
						(IAuthenticationProvider) info.Type.CreateInstance(provider));
					_logHelper.LogInfo("'" + info.Name + "' authentication supported",
						LogEventImpotance.Unimportant);
				}
			}

			var authorProvNames = cfg.AuthorizationProviders;
			if (authorProvNames.Length > 0)
			{
				if (_authorSvc == null)
					throw new ApplicationException("No authorization providers registered.");
				var apl = new List<IAuthorizationProvider>(authorProvNames.Length);
				foreach (var providerName in authorProvNames)
				{
					var info = _authorSvc.GetElement(providerName);
					if (info == null)
						throw new ArgumentException("Provider '" + providerName + "' not registered.");
					apl.Add((IAuthorizationProvider) info.Type.CreateInstance(provider));
					_logHelper.LogInfo("Use '" + providerName + "' authorization provider",
						LogEventImpotance.Unimportant);
				}
				_authorizationProviders = apl.ToArray();
			}
		}

		#region IAuthManager Members
		public IRServerIdentity Authenticate(IRServerCredentials credentials)
		{
			IAuthenticationProvider provider;
			if (!_authenticationProviders.TryGetValue(credentials.AuthenticationType, out provider))
				throw new ArgumentException("Authentication provider '" + credentials.AuthenticationType
					+ "' not registered.");
			if (!provider.Authenticate(credentials.Login, credentials.Password))
				return null;
			return new RServerIdentity(credentials.Login, credentials.AuthenticationType, credentials.User);
		}

		public IRServerPrincipal Authorize(IRServerIdentity identity, string serviceName, Type serviceType,
			string methodName)
		{
			var roles = new Dictionary<string, int>();
			foreach (var provider in _authorizationProviders)
				foreach (var role in provider.Authorize(identity))
					roles[role] = 0;

			return new RServerPrincipal(identity, roles.Keys);
		}
		#endregion

		#region IDisposable Members
		void IDisposable.Dispose()
		{
			// Dispose, if applicable, authentication providers
			foreach (var prov in _authenticationProviders.Values)
			{
				var dispProv = prov as IDisposable;
				if (dispProv != null)
					dispProv.Dispose();
			}
			// Dispose, if applicable, authorization providers
			foreach (var prov in _authorizationProviders)
			{
				var dispProv = prov as IDisposable;
				if (dispProv != null)
					dispProv.Dispose();
			}
		}
		#endregion
	}
}
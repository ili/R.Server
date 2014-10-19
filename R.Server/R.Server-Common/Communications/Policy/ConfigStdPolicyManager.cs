using System.Linq;
#pragma warning disable 1692
using System;
using System.Collections.Generic;
using System.Security.Principal;

using R.Server.ServerModel;

using Rsdn.SmartApp;
using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	[Service(typeof (ConfigStdPolicyManager))]
	public class ConfigStdPolicyManager : ServiceConsumer
	{
		public const string LogSource = "PolicyManager";

		private readonly Dictionary<string, SvcDescriptor> _descriptors =
			new Dictionary<string, SvcDescriptor>();

		private readonly LogHelper _logHelper;

		[ExpectService]
		private IConfigService _cfgSvc;

		public ConfigStdPolicyManager(IServiceProvider provider) : base(provider)
		{
			_logHelper = new LogHelper(provider, LogSource);

			var cfg = _cfgSvc.GetSection<IStdCommPolicyConfig>();
			var commHost = provider.GetService<ICommServicesHost>();
			foreach (var svcCfg in cfg.ServicePolicies)
			{
				if (string.IsNullOrEmpty(svcCfg.Name))
					throw new ApplicationException(
						"Invalid service policy configuration. Service Name not specified.");
				if (!commHost.IsServiceRegistered(svcCfg.Name))
					_logHelper.LogWarning("Invalid service policy configuration. Service '"
						+ svcCfg.Name + "' not registered.");

				_descriptors.Add(svcCfg.Name, new SvcDescriptor(svcCfg.AllowRoles,
					svcCfg.MethodPolicies.ConvertAll(
						mthCfg => new MthDescriptor(mthCfg.Name, mthCfg.AllowRoles))));
			}
		}

		public bool IsPermitted(IRServerPrincipal principal, CommPolicyContext context)
		{
			SvcDescriptor svcDesc;
			if (_descriptors.TryGetValue(context.ServiceName, out svcDesc))
				return svcDesc.IsPermitted(principal, context.MethodName);
			return false;
		}

		#region descriptors classes
		private class SvcDescriptor
		{
			private readonly string[] _allowRoles;
			private readonly IDictionary<string, MthDescriptor> _mthDescs;

			public SvcDescriptor(string[] allowRoles, IEnumerable<MthDescriptor> mthDescs)
			{
				_allowRoles = allowRoles;
				_mthDescs = mthDescs.ToDictionary(desc => desc.Name);
			}

			public bool IsPermitted(IPrincipal principal, string methodName)
			{
				foreach (var role in _allowRoles)
					if (principal.IsInRole(role))
					{
						MthDescriptor md;
						if (_mthDescs.TryGetValue(methodName, out md))
							return md.IsPermitted(principal);
						return true;
					}
				return false;
			}
		}

		private class MthDescriptor
		{
			private readonly string[] _allowRoles;

			public MthDescriptor(string name, string[] allowRoles)
			{
				Name = name;
				_allowRoles = allowRoles;
			}

			public string Name { get; private set; }

			public bool IsPermitted(IPrincipal principal)
			{
				foreach (var role in _allowRoles)
					if (principal.IsInRole(role))
						return true;
				return false;
			}
		}
		#endregion
	}
}
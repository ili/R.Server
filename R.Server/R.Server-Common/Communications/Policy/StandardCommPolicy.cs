#pragma warning disable 1692

using System;
using System.Collections.Generic;
using System.Reflection;

using R.Server.ServerModel;

using Rsdn.SmartApp;
using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	[SecurityPolicy(PolicyName)]
	public class StandardCommPolicy : ServiceConsumer, ISecurityPolicy<CommPolicyContext>
	{
		public const string PolicyName = "Standard";

		private static readonly ElementsCache<Type, ServicePolicyContainer> _servicePolicyContainers =
			new ElementsCache<Type, ServicePolicyContainer>(type => new ServicePolicyContainer(type));

		[ExpectService]
		private IConfigService _cfgSvc;

		private readonly ICommPolicyElement _serverPolicyElement;

		public StandardCommPolicy(IServiceProvider provider) : base(provider)
		{
			var config = _cfgSvc.GetSection<IStdCommPolicyConfig>();
			_serverPolicyElement = new RolesPolicyElement(config.AllowRoles);
		}

		#region ISecurityPolicy<CommPolicyContext> Members
		public bool IsPermitted(IServiceProvider provider, IRServerPrincipal principal,
			CommPolicyContext context)
		{
			// Three level policy check
			// First check server-wide policy (policy for access all server services)
			if (!_serverPolicyElement.IsPermitted(provider, principal, context))
				return false;

			// Next check whole service policy
			var policyContainer = _servicePolicyContainers.Get(context.ServiceType);
			if (!policyContainer.GetServicePolicy().IsPermitted(provider, principal, context))
				return false;

			// Next check method policy
			if (!policyContainer.GetMethodPolicy(context.MethodName)
				.IsPermitted(provider, principal, context))
				return false;

			return true;
		}
		#endregion

		#region ServicePolicyContainer class
		private class ServicePolicyContainer
		{
			private readonly Type _serviceType;
			private readonly ICommPolicyElement _servicePolicy;
			private readonly ElementsCache<string, ICommPolicyElement> _methodPolicies;

			public ServicePolicyContainer(Type serviceType)
			{
				_serviceType = serviceType;
				_servicePolicy = new CombinedPolicyElement(GetAttributePolicies(_serviceType));
				_methodPolicies = new ElementsCache<string, ICommPolicyElement>(CreateMethodPolicy);
			}

			private static ICommPolicyElement[] GetAttributePolicies(ICustomAttributeProvider attrProvider)
			{
				var attrPolicies = new List<ICommPolicyElement>();
				foreach (var attr in attrProvider.GetCustomAttributes(true))
				{
					var policy = attr as ICommPolicyElement;
					if (policy != null)
						attrPolicies.Add(policy);
				}
				return attrPolicies.ToArray();
			}

			private ICommPolicyElement CreateMethodPolicy(string methodName)
			{
				var info = _serviceType.GetMethod(methodName);
				if (info == null)
					throw new ArgumentException("Could not find method '" + methodName
						+ "' in service '" + _serviceType.FullName + "'.");
				var policies = GetAttributePolicies(info);
				// If method policy absent - allow all operations.
				// It's differ from service policy! If service policy absent - all operation will be denied.
				return policies.Length > 0
					? new CombinedPolicyElement(policies)
					: (ICommPolicyElement) new AllowAllPolicyElement();
			}

			public ICommPolicyElement GetServicePolicy()
			{
				return _servicePolicy;
			}

			public ICommPolicyElement GetMethodPolicy(string methodName)
			{
				return _methodPolicies.Get(methodName);
			}
		}
		#endregion
	}
}
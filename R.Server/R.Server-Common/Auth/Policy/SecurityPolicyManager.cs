#pragma warning disable 1692
using System;
using System.Collections.Generic;
using System.Linq;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	/// <summary>
	/// Standard <see cref="ISecurityPolicyManager"/> implementation.
	/// </summary>
	[Service(typeof (ISecurityPolicyManager))]
	public class SecurityPolicyManager : ServiceConsumer, ISecurityPolicyManager, IDisposable
	{
		[ExpectService]
		private IRegKeyedElementsService<string, SecurityPolicyInfo> _policiesSvc;

		private readonly ElementsCache<string, HashSet<Type>> _contextTypes;
		private readonly ElementsCache<string, object> _policies;

		public SecurityPolicyManager(IServiceProvider provider) : base(provider)
		{
			_contextTypes = new ElementsCache<string, HashSet<Type>>(
				name => new HashSet<Type>(_policiesSvc.GetElement(name).ContextTypes));
			_policies = new ElementsCache<string, object>(
				name => _policiesSvc.GetElement(name).Type.CreateInstance(provider));
		}

		#region ISecurityPolicyManager Members
		public bool IsPermitted<C>(IServiceProvider provider, IRServerPrincipal principal,
			C context, params string[] policyNames)
		{
			var ctxType = typeof (C);
			foreach (var policyName in policyNames)
			{
				if (!_policiesSvc.ContainsElement(policyName))
					throw new ArgumentException("Security policy '" + policyName + "' not registered.");
				if (!_contextTypes.Get(policyName).Contains(ctxType))
					throw new ArgumentException("Security policy '" + policyName + "' does not support '"
						+ ctxType.FullName + "' context.");
				var policy = (ISecurityPolicy<C>) _policies.Get(policyName);
				if (!policy.IsPermitted(provider, principal, context))
					return false;
			}
			return true;
		}
		#endregion

		#region IDisposable Members
		public void Dispose()
		{
			_policies.Keys
				.Select(key => _policies.Get(key))
				.OfType<IDisposable>()
				.ForEach(d => d.Dispose());
		}
		#endregion
	}
}
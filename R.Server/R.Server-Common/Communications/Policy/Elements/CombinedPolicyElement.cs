using System;

using R.Server.ServerModel;

namespace R.Server.Common
{
	internal class CombinedPolicyElement : ICommPolicyElement
	{
		private readonly ICommPolicyElement[] _policies;

		public CombinedPolicyElement(params ICommPolicyElement[] policies)
		{
			_policies = policies;
		}

		#region IAuthPolicy Members
		/// <summary>
		/// Define supplied principal permission.
		/// </summary>
		public bool IsPermitted(
			IServiceProvider provider,
			IRServerPrincipal principal,
			CommPolicyContext context)
		{
			if (_policies.Length == 0)
				return false;
			foreach (var policy in _policies)
				if (!policy.IsPermitted(provider, principal, context))
					return false;
			return true;
		}
		#endregion
	}
}
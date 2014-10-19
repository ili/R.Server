using System;

namespace R.Server.ServerModel
{
	public interface ISecurityPolicyManager
	{
		bool IsPermitted<C>(
			IServiceProvider provider,
			IRServerPrincipal principal,
			C context,
			params string[] policyNames);
	}
}
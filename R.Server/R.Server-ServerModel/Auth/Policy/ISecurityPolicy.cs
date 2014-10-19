using System;

namespace R.Server.ServerModel
{
	public interface ISecurityPolicy<C>
	{
		bool IsPermitted(IServiceProvider provider, IRServerPrincipal principal, C context);
	}
}
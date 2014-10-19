using System;

namespace R.Server.ServerModel
{
	public interface IAuthManager
	{
		IRServerIdentity Authenticate(IRServerCredentials credentials);

		IRServerPrincipal Authorize(IRServerIdentity identity, string serviceName, Type serviceType,
			string methodName);
	}
}
using System.Security.Principal;

namespace R.Server.ServerModel
{
	public interface IRServerIdentity : IIdentity
	{
		string User { get; }
	}
}
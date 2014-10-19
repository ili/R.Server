using System.ServiceModel;

namespace R.Server.ClientModel
{
	/// <summary>
	/// Contract for R.Server core.
	/// </summary>
	[ServiceContract]
	public interface IRServerInfoService
	{
		[OperationContract]
		RServerInfo GetInfo();
	}
}
using System.ServiceModel;

namespace R.Server.ClientModel
{
	/// <summary>
	/// Remote access to TaskManager
	/// </summary>
	[ServiceContract]
	public interface ITaskManagerCommService
	{
		[OperationContract]
		TaskResult Run(string taskName, TaskParameter[] taskParams);
	}
}
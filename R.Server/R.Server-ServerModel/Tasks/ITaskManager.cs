using R.Server.ClientModel;

namespace R.Server.ServerModel
{
	public interface ITaskManager
	{
		bool IsTaskExists(string taskName);

		TaskResult RunTask(string taskName, TaskParameter[] taskParams);
	}
}
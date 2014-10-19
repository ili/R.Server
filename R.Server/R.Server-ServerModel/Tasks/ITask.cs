using R.Server.ClientModel;

namespace R.Server.ServerModel
{
	/// <summary>
	/// Server task interface
	/// </summary>
	public interface ITask
	{
		string Run(TaskParameter[] taskParams);
	}
}
using System;

using R.Server.ClientModel;
using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	using ITasksSvc = IRegKeyedElementsService<string, TaskInfo>;

	/// <summary>
	/// Standard <see cref="ITaskManager"/> implementation.
	/// </summary>
	[Service(typeof (ITaskManager))]
	public class TaskManager : ITaskManager
	{
		private readonly IServiceProvider _provider;
		private readonly ITasksSvc _tasksSvc;

		public TaskManager(IServiceProvider provider)
		{
			_provider = provider;
			_tasksSvc = provider.GetService<ITasksSvc>();
		}

		#region ITaskManager Members
		public bool IsTaskExists(string taskName)
		{
			return _tasksSvc != null && _tasksSvc.ContainsElement(taskName);
		}

		public TaskResult RunTask(string taskName, TaskParameter[] taskParams)
		{
			if (!IsTaskExists(taskName))
				throw new ArgumentException("Could not find task '" + taskName + "'", "taskName");
			var tsk = (ITask) _tasksSvc.GetElement(taskName).Type.CreateInstance(_provider);
			try
			{
				return new TaskResult(true, tsk.Run(taskParams), "");
			}
			catch (Exception ex)
			{
				return new TaskResult(false, null, ex.Message);
			}
		}
		#endregion
	}
}
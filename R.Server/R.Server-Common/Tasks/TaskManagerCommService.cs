using System;
using System.ServiceModel;

using R.Server.ClientModel;
using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	/// <summary>
	/// Standard <see cref="ITaskManagerCommService"/> implementation.
	/// </summary>
	[RCommService(ServiceName, typeof (ITaskManagerCommService),
		InstanceContextMode.Single)]
	public class TaskManagerCommService : ITaskManagerCommService
	{
		private readonly IServiceProvider _provider;
		public const string ServiceName = "TaskManager";

		public TaskManagerCommService(IServiceProvider provider)
		{
			_provider = provider;
		}

		#region ITaskManagerCommService Members
		public TaskResult Run(string taskName, TaskParameter[] taskParams)
		{
			var tm = _provider.GetService<ITaskManager>();
			if (tm == null)
				throw new ServiceNotFoundException(typeof (ITaskManager));
			return tm.RunTask(taskName, taskParams);
		}
		#endregion
	}
}
/*
 * Created by: Eugene Agafonov
 * Created: 2 мая 2007 г.
 */

using System;
using System.Collections.Generic;

using R.Server.ClientModel;
using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	/// <summary>
	/// Hosts task, which has been run by scheduler
	/// </summary>
	public class ScheduledTaskHost
	{
		public const string LogSource = "ScheduledTaskHost";
		private readonly IServiceProvider _provider;
		private readonly LogHelper _logHelper;
		private readonly ISchedulerJobConfig _taskConfig;

		public DateTime StartTime { get; private set; }

		public ScheduledTaskHost(IServiceProvider provider, ISchedulerJobConfig taskConfig)
		{
			_provider = provider;
			_logHelper = new LogHelper(_provider, LogSource);
			_taskConfig = taskConfig;
			StartTime = DateTime.Now;
		}

		public void Start()
		{
			var tm = _provider.GetRequiredService<ITaskManager>();
			if (null == tm) throw new ServiceNotFoundException(typeof (ITaskManager));

			_logHelper.LogInfo(string.Format(Messages.ScheduledTaskStartedMessageTemplate,
				_taskConfig.Task));
			var result = tm.RunTask(_taskConfig.Task, GetTaskParametersFromConfig());
			if (result.IsSuccess)
				_logHelper.LogInfo(string.Format(Messages.ScheduledTaskCompletedMessageTemplate,
					_taskConfig.Task, DateTime.Now));
			else
				_logHelper.LogError(string.Format(
					Messages.ScheduledTaskFailedMessageTemplate,
					_taskConfig.Task, result.ErrorMessage));
		}

		private TaskParameter[] GetTaskParametersFromConfig()
		{
			var lst = new List<TaskParameter>();
			foreach (var cfg in _taskConfig.Parameters)
			{
				lst.Add(new TaskParameter(cfg.Name, cfg.Value));
			}
			return lst.ToArray();
		}
	}
}
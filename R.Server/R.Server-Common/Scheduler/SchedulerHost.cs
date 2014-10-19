/*
 * Created by: Eugene Agafonov
 * Created: 22 апреля 2007 г.
 */

using System;
using System.Collections.Generic;
using System.Threading;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	/// <summary>
	/// Scheduler host which actually runs scheduled tasks.
	/// </summary>
	public class SchedulerHost : IDisposable
	{
		public const string LogSource = "SchedulerHost";

		private readonly IServiceProvider _provider;
		private readonly ISchedulerConfig _schedulerConfig;
		private readonly LogHelper _logHelper;
		private readonly Dictionary<string, Thread> _taskThreads;
		private readonly Dictionary<string, ScheduledTaskHost> _taskHosts;
		private readonly ManualResetEvent _stopEvent;
		private Timer _timer;
		private volatile int _lastMinute = -1;
		private readonly object _runLockFlag = new object();

		public SchedulerHost(IServiceProvider provider, ISchedulerConfig config)
		{
			_provider = provider;
			_schedulerConfig = config;

			_logHelper = new LogHelper(_provider, LogSource);
			_taskThreads = new Dictionary<string, Thread>();
			_taskHosts = new Dictionary<string, ScheduledTaskHost>();
			_stopEvent = new ManualResetEvent(false);

			foreach (var taskConfig in _schedulerConfig.Jobs)
			{
				var cron = new CronPeriod(taskConfig.Period);
				if (cron.IsWrongEntry)
				{
					_logHelper.LogWarning(string.Format(
						Messages.WrongCronEntryMessageTemplate, taskConfig.Task));
					// I think no need to throw an exception
				}
			}
		}

		/// <summary>
		/// Start scheduler host
		/// </summary>
		public void Start()
		{
			try
			{
				_timer = new Timer(OnTimerTick, null, TimeSpan.FromMinutes(1),
					TimeSpan.FromSeconds(59));

				_stopEvent.WaitOne();
			}
			catch (Exception ex)
			{
				_logHelper.LogWarning(
					string.Format(Messages.SchedulerHostExceptionWarningTemplate,
						LogSource, ex.Message),
					LogEventImpotance.Important);
			}
			finally
			{
				Dispose();
			}
		}


		public void OnTimerTick(object stateInfo)
		{
			if (null != _schedulerConfig
				&& null != _schedulerConfig.Jobs
					&& _schedulerConfig.Jobs.Length > 0)
			{
				try
				{
					var now = DateTime.Now;

					if (_lastMinute != now.Minute)
						lock (_runLockFlag)
							if (_lastMinute != now.Minute)
							{
								_lastMinute = now.Minute;

								// run new tasks if nessesary
								RunTasks(now);

								// check older tasks did they succeed
								CheckOlderTasks(now);
							}
				}
				catch (Exception ex)
				{
					_logHelper.LogWarning(
						string.Format(Messages.SchedulerHostExceptionWarningTemplate,
							LogSource, ex.Message),
						LogEventImpotance.Important);

					RequestStop();
				}
			}
			else
			{
				_logHelper.LogWarning(Messages.SchedulerHostNoTasksFoundMessage,
					LogEventImpotance.Important);
				RequestStop();
			}
		}

		/// <summary>
		/// Stop scheduler host
		/// </summary>
		public void RequestStop()
		{
			_stopEvent.Set();
		}

		private void RunTasks(DateTime now)
		{
			var tm = _provider.GetService<ITaskManager>();
			if (null == tm)
				throw new ServiceNotFoundException(typeof (ITaskManager));

			foreach (var taskConfig in _schedulerConfig.Jobs)
			{
				// We already know that every configured task is corresponding to a system task, since scheduler			
				// allowed this to run.
				// so here we look which task is ready to run and run new worker thread 

				var cron = new CronPeriod(taskConfig.Period);
				if (cron.IsTimeToStart(now))
				{
					// start task and add it to the task list
					var host = new ScheduledTaskHost(_provider, taskConfig);
					var thread = new Thread(host.Start);
					thread.Start();

					_taskHosts[taskConfig.Task] = host;
					_taskThreads[taskConfig.Task] = thread;
				}
			}
		}

		private void CheckOlderTasks(DateTime now)
		{
			// here we iterate through tasks and tasks results and 
			// know which are running, which succeeded and which failed.
			foreach (var taskName in _taskThreads.Keys)
			{
				var thread = _taskThreads[taskName];
				if (!thread.IsAlive)
				{
					thread.Join(TimeSpan.FromSeconds(10));
					_taskHosts.Remove(taskName);
				}
				else if (_taskHosts[taskName].StartTime > now.AddMinutes(30))
				{
					// if task work more that some time, perhaps kill it?
					// more accurate idea is either to include kill time in config,
					// or to make TaskHost timered and starting a separate thread,
				}
			}

			// we cannot change collection while iterating through it with enumerator
			// so we just synchronize it now.
			if (_taskHosts.Keys.Count < 1)
			{
				_taskThreads.Clear();
			}
			else
			{
				foreach (var taskName in _taskHosts.Keys)
				{
					if (_taskThreads.ContainsKey(taskName))
					{
						_taskThreads.Remove(taskName);
					}
				}
			}
		}

		#region IDisposable Members
		public void Dispose()
		{
			if (_timer != null)
				_timer.Dispose();
			if (_stopEvent != null)
				_stopEvent.Close();
		}
		#endregion
	}
}
#pragma warning disable 1692
/*
 * Created by: Eugene Agafonov
 * Created: 1 апреля 2007 г.
 */

using System;
using System.Threading;

using R.Server.ServerModel;

using Rsdn.SmartApp;
using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	/// <summary>
	/// Scheduler service
	/// </summary>
	[Service(typeof (IScheduler))]
	public class Scheduler : ServiceConsumer, IScheduler, IDisposable
	{
		public const string LogSource = "Scheduler";

		private readonly IServiceProvider _provider;
		private readonly LogHelper _logHelper;
		private readonly ISchedulerConfig _schedulerConfig;
		private readonly SchedulerHost _schedulerHost;
		private readonly Thread _schedulerHostThread;

		[ExpectService]
		private IConfigService _cfgSvc;

		[ExpectService]
		private IRServer _rServer;

		[ExpectService]
		private ITaskManager _taskManager;

		public Scheduler(IServiceProvider provider) : base(provider)
		{
			if (null == provider)
				throw new ArgumentNullException("provider");

			_provider = provider;
			_logHelper = new LogHelper(_provider, LogSource);

			_schedulerConfig = _cfgSvc.GetSection<ISchedulerConfig>();
			if (null == _schedulerConfig)
				throw new ApplicationException(Messages.SchedulerConfigNotFound);

			_schedulerHost = new SchedulerHost(_provider, _schedulerConfig);
			_schedulerHostThread = new Thread(_schedulerHost.Start);

			_rServer.Started += sender => Start();
			_rServer.Stopped += sender => Stop();
		}

		#region IScheduler Members
		/// <summary>
		/// Start scheduler host thread which actually parses configs and runs tasks
		/// </summary>
		public void Start()
		{
			if (null == _schedulerHostThread || _schedulerHostThread.IsAlive)
				return;
			foreach (var job in _schedulerConfig.Jobs)
			{
				if (!_taskManager.IsTaskExists(job.Task))
				{
					throw new ApplicationException(
						string.Format(Messages.ScheduledTaskNotExists, job.Task));
				}
			}

			_logHelper.LogInfo(
				string.Format(Messages.SchedulerStartingMessageTemplate, _schedulerConfig.Jobs.Length),
				LogEventImpotance.Unimportant);

			_schedulerHostThread.Start();
			// wait until scheduler host thread starts
			while (!_schedulerHostThread.IsAlive)
			{
			}

			_logHelper.LogInfo(Messages.SchedulerStartedMessage, LogEventImpotance.Unimportant);
		}

		/// <summary>
		/// Stops scheduler host thread
		/// </summary>
		public void Stop()
		{
			if (null != _schedulerHostThread && _schedulerHostThread.IsAlive)
			{
				_logHelper.LogInfo(Messages.SchedulerStoppingMessage, LogEventImpotance.Unimportant);
				_schedulerHost.RequestStop();
				_schedulerHostThread.Join();
				_logHelper.LogInfo(Messages.SchedulerStoppedMessage);
			}
		}
		#endregion

		#region IDisposable Members
		public void Dispose()
		{
			Stop();
		}
		#endregion
	}
}
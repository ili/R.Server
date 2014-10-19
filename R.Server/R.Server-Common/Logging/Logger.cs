#pragma warning disable 1692

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using R.Server.ServerModel;

using Rsdn.SmartApp;
using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	/// <summary>
	/// Logger implementation.
	/// </summary>
	/// <remarks>
	/// You must call <see cref="Dispose"/> method before application exits,
	/// because of writing thread will prevent application from terminating.
	/// </remarks>
	[Service(typeof (ILogger))]
	public class Logger : ServiceConsumer, ILogger, IDisposable
	{
		public const string LogSource = "Logger";

		private readonly List<ILogTarget> _targets = new List<ILogTarget>();

		private readonly Queue<LogEventContainer> _eventQueue =
			new Queue<LogEventContainer>();

		private readonly EventWaitHandle _eventQueueEvent =
			new EventWaitHandle(false, EventResetMode.AutoReset);

		private volatile bool _stopFlag;
		private readonly Thread _writeThread;

		private readonly Dictionary<ILogTarget, string> _targetNames =
			new Dictionary<ILogTarget, string>();

		[ExpectService]
		private IConfigService _cfgSvc;

		[ExpectService(Required = false)]
		private IRegKeyedElementsService<string, LogTargetInfo> _targetsSvc;

		public Logger(IServiceProvider provider) : base(provider)
		{
			var lc = _cfgSvc.GetSection<ILoggerConfig>();
			foreach (var targetCfg in lc.Targets)
			{
				if (string.IsNullOrEmpty(targetCfg.Name))
					throw new ApplicationException("Target name cannot be null or empty.");
				if (_targetsSvc == null || !_targetsSvc.ContainsElement(targetCfg.Name))
					throw new ApplicationException("Unknown log target '" + targetCfg.Name + "'.");
				var targetParams = targetCfg.Parameters.ConvertAll(
					cfg => new TargetParameter(cfg.Name, cfg.Value));
				var targetInfo = _targetsSvc.GetElement(targetCfg.Name);
				var target = (ILogTarget) targetInfo.Type.CreateInstance(provider,
					new InstancingCustomParam("logger", this, true),
					new InstancingCustomParam("targetParams", targetParams, true));
				_targets.Add(target);
				_targetNames.Add(target, targetCfg.Name);
			}

			//_rServer.Disposing += sender => Dispose();
			_writeThread = new Thread(WriteEventToTargets);
			_writeThread.Start();
		}

		private void WriteEventToTargets()
		{
			var badTargets = new List<ILogTarget>(_targets.Count);
			var badTargetMessages = new List<string>(_targets.Count);
			while (true)
			{
				_eventQueueEvent.WaitOne();
				while (_eventQueue.Count > 0)
				{
					LogEventContainer cont;
					lock (_eventQueue)
						cont = _eventQueue.Dequeue();
					badTargets.Clear();
					badTargetMessages.Clear();
					foreach (var target in _targets)
						try
						{
							target.WriteEvent(cont.EventInfo, cont.EventDate);
						}
						catch (Exception ex)
						{
							badTargets.Add(target);
							badTargetMessages.Add(ex.Message);
						}
					for (var i = 0; i < badTargets.Count; i++)
					{
						string name;
						lock (_targets)
						{
							var dispTargets = badTargets[i] as IDisposable;
							_targets.Remove(badTargets[i]);
							name = _targetNames[badTargets[i]];
							_targetNames.Remove(badTargets[i]);
							if (dispTargets != null)
								dispTargets.Dispose();
						}
						LogEvent(new LogEventInfo(
							"'" + name + "' target throw an exception and will be removed. "
								+ badTargetMessages[i],
							LogSource,
							LogEventImpotance.VeryImportant,
							LogEventType.Error));
					}
				}
				if (_stopFlag)
					return;
			}
		}

		#region ILogger Members
		public void LogEvent(LogEventInfo eventInfo)
		{
			lock (_eventQueue)
				_eventQueue.Enqueue(new LogEventContainer(eventInfo, DateTime.Now));
			_eventQueueEvent.Set();
		}
		#endregion

		#region IDisposable Members
		///<summary>
		/// Performs application-defined tasks associated with freeing, releasing,
		/// or resetting unmanaged resources.
		///</summary>
		public void Dispose()
		{
			_stopFlag = true;
			_eventQueueEvent.Set();
			_writeThread.Join();
			_targets.OfType<IDisposable>().ForEach(d => d.Dispose());
		}
		#endregion

		#region LogEventContainer class
		private class LogEventContainer
		{
			public LogEventContainer(LogEventInfo eventInfo, DateTime eventDate)
			{
				EventInfo = eventInfo;
				EventDate = eventDate;
			}

			public LogEventInfo EventInfo { get; private set; }
			public DateTime EventDate { get; private set; }
		}
		#endregion
	}
}
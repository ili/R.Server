#pragma warning disable 1692

using System;
using System.Diagnostics;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	[LogTarget(TargetName)]
	public class WindowsLogTarget : ServiceConsumer, ILogTarget, IDisposable
	{
		public const string TargetName = "Windows";
		public const string LogName = @"R.Server";
		public const string SourceNamePrefix = "R.Server$";

		private const string _messageTemplate = "[{0}] {1}";

		[ExpectService]
		private IRServer _rServer;

		private readonly EventLog _eventLog;

		public WindowsLogTarget(IServiceProvider provider) : base(provider)
		{
			var source = SourceNamePrefix + _rServer.Name;
			CreateLog(source);
			_eventLog = new EventLog {Source = source, Log = LogName};
		}

		private static void CreateLog(string source)
		{
			if (!EventLog.SourceExists(source))
				EventLog.CreateEventSource(source, LogName);
		}

		private static EventLogEntryType RServerType2WindowsType(LogEventType logEventType)
		{
			switch (logEventType)
			{
				case LogEventType.Information:
					return EventLogEntryType.Information;
				case LogEventType.Warning:
					return EventLogEntryType.Warning;
				case LogEventType.Error:
					return EventLogEntryType.Error;
				case LogEventType.CriticalError:
					return EventLogEntryType.Error;
				default:
					throw new ArgumentException("Invalid event type", "logEventType");
			}
		}

		#region ILogTarget Members
		public void WriteEvent(LogEventInfo eventInfo, DateTime eventDate)
		{
			_eventLog.WriteEntry(
				string.Format(_messageTemplate, eventInfo.Source, eventInfo.Message),
				RServerType2WindowsType(eventInfo.Type),
				0,
				(short) eventInfo.Impotance);
		}
		#endregion

		#region IDisposable Members
		public void Dispose()
		{
			_eventLog.Dispose();
		}
		#endregion
	}
}
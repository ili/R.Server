using System;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	public class LogHelper
	{
		private readonly string _source;
		private readonly ILogger _logger;

		public LogHelper(IServiceProvider provider, string source)
		{
			_logger = provider.GetService<ILogger>();
			_source = source;
		}

		public void Log(LogEventType eventType, string message, LogEventImpotance impotance)
		{
			if (_logger != null)
				_logger.LogEvent(new LogEventInfo(message, _source, impotance,
					eventType));
		}

		public void LogInfo(string message, LogEventImpotance impotance)
		{
			Log(LogEventType.Information, message, impotance);
		}

		public void LogInfo(string message)
		{
			LogInfo(message, LogEventImpotance.Important);
		}

		public void LogWarning(string message, LogEventImpotance impotance)
		{
			Log(LogEventType.Warning, message, impotance);
		}

		public void LogWarning(string message)
		{
			LogWarning(message, LogEventImpotance.Important);
		}

		public void LogError(string message, bool isCritical, LogEventImpotance impotance)
		{
			Log(isCritical ? LogEventType.CriticalError : LogEventType.Error, message, impotance);
		}

		public void LogError(string message, bool isCritical)
		{
			LogError(message, isCritical, LogEventImpotance.Important);
		}

		public void LogError(string message, LogEventImpotance impotance)
		{
			LogError(message, false, impotance);
		}

		public void LogError(string message)
		{
			LogError(message, false);
		}
	}
}
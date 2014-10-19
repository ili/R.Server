namespace R.Server.ServerModel
{
	public class LogEventInfo
	{
		public LogEventInfo(string message, string source, LogEventImpotance impotance,
			LogEventType type)
		{
			Message = message;
			Source = source;
			Impotance = impotance;
			Type = type;
		}

		public string Message { get; private set; }

		public string Source { get; private set; }

		public LogEventImpotance Impotance { get; private set; }

		public LogEventType Type { get; private set; }
	}
}
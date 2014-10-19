namespace R.Server.ServerModel
{
	/// <summary>
	/// Log service.
	/// </summary>
	public interface ILogger
	{
		void LogEvent(LogEventInfo eventInfo);
	}
}
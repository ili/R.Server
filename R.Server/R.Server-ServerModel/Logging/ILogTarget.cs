using System;

namespace R.Server.ServerModel
{
	/// <summary>
	/// Logging target.
	/// </summary>
	public interface ILogTarget
	{
		/// <summary>
		/// Write event to target
		/// </summary>
		void WriteEvent(LogEventInfo eventInfo, DateTime eventDate);
	}
}
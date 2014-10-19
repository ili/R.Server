using System;

using R.Server.ServerModel;

namespace R.Server.Common
{
	internal class LogAccessor : AccessorBase
	{
		public LogAccessor(IServiceProvider provider, string dbName)
			: base(provider, dbName)
		{
		}

		public void AddEvent(LogEventInfo eventInfo, DateTime eventDate)
		{
			using (var con = CreateConnection())
			using (var db = new LogDataContext(con))
				db.AddEvent(eventDate, (int) eventInfo.Type, (int) eventInfo.Impotance, eventInfo.Source,
					eventInfo.Message);
		}
	}
}
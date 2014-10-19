#pragma warning disable 1692
using System;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	[LogTarget(TargetName)]
	public class DBLogTarget : ILogTarget
	{
		public const string TargetName = "Database";
		public const string DBParamName = "db-name";

		private readonly LogAccessor _accessor;

#pragma warning disable SuggestBaseTypeForParameter
		public DBLogTarget(IServiceProvider provider, TargetParameter[] targetParams)
#pragma warning restore SuggestBaseTypeForParameter
		{
			var paramsMap = targetParams.Convert2Dictionary(p => p.Name);
			if (!paramsMap.ContainsKey(DBParamName))
				throw new ApplicationException("Required parameter '" + DBParamName
					+ "' not supplied for database log target.");

			_accessor = new LogAccessor(provider, paramsMap[DBParamName].Value);
		}

		#region ILogTarget Members
		public void WriteEvent(LogEventInfo eventInfo, DateTime eventDate)
		{
			_accessor.AddEvent(eventInfo, eventDate);
		}
		#endregion
	}
}
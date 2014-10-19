using System;
using System.Linq;
using R.Server.ClientModel;
using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	/// <summary>
	/// Test <see cref="ITask"/> implementation.
	/// </summary>
	[Task(TaskName)]
	public class EchoTask : ITask
	{
		public const string TaskName = "Echo";
		public const string MessageParamName = "Message";
		public const string LogParamName = "Log";
		public const string LogSource = "EchoTask";

		private readonly LogHelper _logHelper;

		public EchoTask(IServiceProvider provider)
		{
			_logHelper = new LogHelper(provider, LogSource);
		}

		#region ITask Members
		public string Run(TaskParameter[] taskParams)
		{
			var tpDic = taskParams.ToDictionary(tp => tp.Name);
			if (!tpDic.ContainsKey(MessageParamName))
				throw new ArgumentException("Required parameter '" + MessageParamName + "' not supplied");
			if (tpDic.ContainsKey(LogParamName))
				_logHelper.LogInfo(tpDic[MessageParamName].Value);
			return tpDic[MessageParamName].Value;
		}
		#endregion
	}
}
using System;

using Rsdn.SmartApp;

namespace R.Server.ServerModel
{
	public class TaskInfo : NamedElementInfo
	{
		public TaskInfo(string taskName, Type taskType) : base(taskName, taskType)
		{
		}
	}
}
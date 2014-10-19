using System;

using Rsdn.SmartApp;

namespace R.Server.ServerModel
{
	[AttributeUsage(AttributeTargets.Class)]
	public class TaskAttribute : NamedElementAttribute
	{
		public TaskAttribute(string taskName) : base(taskName)
		{
		}
	}
}
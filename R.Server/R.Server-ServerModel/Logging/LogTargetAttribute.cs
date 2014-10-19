using System;

using Rsdn.SmartApp;

namespace R.Server.ServerModel
{
	/// <summary>
	/// Attribute for mark log target implemetations.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class LogTargetAttribute : NamedElementAttribute
	{
		public LogTargetAttribute(string name) : base(name)
		{
		}
	}
}
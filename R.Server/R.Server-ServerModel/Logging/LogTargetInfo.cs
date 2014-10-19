using System;

using Rsdn.SmartApp;

namespace R.Server.ServerModel
{
	public class LogTargetInfo : NamedElementInfo
	{
		public LogTargetInfo(string name, Type type)
			: base(name, "", type)
		{
		}
	}
}
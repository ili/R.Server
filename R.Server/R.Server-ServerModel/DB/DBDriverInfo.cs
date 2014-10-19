using System;

using Rsdn.SmartApp;

namespace R.Server.ServerModel
{
	public class DBDriverInfo : NamedElementInfo
	{
		public DBDriverInfo(string name, Type type)
			: base(name, "", type)
		{
		}
	}
}
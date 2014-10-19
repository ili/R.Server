using System;

using Rsdn.SmartApp;

namespace R.Server.ServerModel
{
	public class SecurityPolicyInfo : NamedElementInfo
	{
		public SecurityPolicyInfo(string name, Type type, Type[] contextTypes)
			: base(name, "", type)
		{
			ContextTypes = contextTypes;
		}

		public Type[] ContextTypes { get; private set; }
	}
}
using System;

using Rsdn.SmartApp;

namespace R.Server.ServerModel
{
	public class AuthenticationProviderInfo : NamedElementInfo
	{
		public AuthenticationProviderInfo(string name, Type type)
			: base(name, "", type)
		{
		}
	}
}
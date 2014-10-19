using System;

using Rsdn.SmartApp;

namespace R.Server.ServerModel
{
	[AttributeUsage(AttributeTargets.Class)]
	public class AuthenticationProviderAttribute : NamedElementAttribute
	{
		public AuthenticationProviderAttribute(string name) : base(name)
		{
		}
	}
}
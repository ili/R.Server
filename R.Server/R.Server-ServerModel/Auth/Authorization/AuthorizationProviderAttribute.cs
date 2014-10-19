using System;

using Rsdn.SmartApp;

namespace R.Server.ServerModel
{
	[AttributeUsage(AttributeTargets.Class)]
	public class AuthorizationProviderAttribute : NamedElementAttribute
	{
		public AuthorizationProviderAttribute(string name) : base(name)
		{
		}
	}
}
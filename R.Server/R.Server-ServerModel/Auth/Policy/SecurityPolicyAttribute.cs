using System;

using Rsdn.SmartApp;

namespace R.Server.ServerModel
{
	[AttributeUsage(AttributeTargets.Class)]
	public class SecurityPolicyAttribute : NamedElementAttribute
	{
		public SecurityPolicyAttribute(string name) : base(name)
		{
		}
	}
}
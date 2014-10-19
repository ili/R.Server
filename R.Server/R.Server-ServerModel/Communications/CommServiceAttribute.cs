using System;

using JetBrains.Annotations;

using Rsdn.SmartApp;

namespace R.Server.ServerModel
{
	[AttributeUsage(AttributeTargets.Class)]
	[MeansImplicitUse]
	public class CommServiceAttribute : NamedElementAttribute
	{
		public CommServiceAttribute(string name, Type contractType) : base(name)
		{
			ContractType = contractType;
		}

		public Type ContractType { get; private set; }
	}
}
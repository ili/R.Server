using System;

using Rsdn.SmartApp;

namespace R.Server.ServerModel
{
	public class CommServiceInfo : NamedElementInfo
	{
		public CommServiceInfo(string name, Type type, Type contractType)
			: base(name, type)
		{
			ContractType = contractType;
		}

		public Type ContractType { get; private set; }
	}
}
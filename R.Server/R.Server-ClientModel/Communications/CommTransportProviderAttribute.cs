using System;

namespace R.Server.ServerModel
{
	[AttributeUsage(AttributeTargets.Class)]
	public class CommTransportProviderAttribute : Attribute
	{
		private readonly string _name;

		public CommTransportProviderAttribute(string name)
		{
			_name = name;
		}

		public string Name
		{
			get { return _name; }
		}
	}
}
using System;

using Rsdn.SmartApp;

namespace R.Server.ServerModel
{
	public class CommTransportProviderInfo : IKeyedElementInfo<string>
	{
		private readonly string _name;
		private readonly Type _type;

		public CommTransportProviderInfo(string name, Type type)
		{
			_name = name;
			_type = type;
		}

		public string Name
		{
			get { return _name; }
		}

		public Type Type
		{
			get { return _type; }
		}

		#region IKeyedElementInfo<string> Members
		/// <summary>
		/// Ключ.
		/// </summary>
		string IKeyedElementInfo<string>.Key
		{
			get { return Name; }
		}
		#endregion
	}
}
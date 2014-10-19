using System;

namespace R.Client.Model
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ClientApplicationAttribute : Attribute
	{
		private readonly string _name;
		private readonly string _displayName;
		private readonly string _description;

		public ClientApplicationAttribute(string name, string displayName, string description)
		{
			_name = name;
			_displayName = displayName;
			_description = description;
		}

		public ClientApplicationAttribute(string name, string displayName)
			: this(name, displayName, null)
		{}

		public string Name
		{
			get { return _name; }
		}

		public string DisplayName
		{
			get { return _displayName; }
		}

		public string Description
		{
			get { return _description; }
		}
	}
}

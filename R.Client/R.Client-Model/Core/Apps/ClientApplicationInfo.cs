using System;

using Rsdn.SmartApp;

namespace R.Client.Model
{
	public class ClientApplicationInfo : IKeyedElementInfo<string>
	{
		private readonly Type _appType;
		private readonly string _name;
		private readonly string _displayName;
		private readonly string _description;

		public ClientApplicationInfo(Type appType, string name, string displayName, string description)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException(name);
			if (string.IsNullOrEmpty(displayName))
				throw new ArgumentNullException(displayName);
			_appType = appType;
			_name = name;
			_displayName = displayName;
			_description = description;
		}

		public Type AppType
		{
			get { return _appType; }
		}

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

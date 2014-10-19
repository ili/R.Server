namespace R.Client.Model
{
	/// <summary>
	/// Application entry point.
	/// </summary>
	public class AppEntryPoint
	{
		private readonly string _name;
		private readonly string _displayName;
		private readonly bool _isEnabled;

		public AppEntryPoint(string name, string displayName, bool isEnabled)
		{
			_name = name;
			_displayName = displayName;
			_isEnabled = isEnabled;
		}

		public string Name
		{
			get { return _name; }
		}

		public string DisplayName
		{
			get { return _displayName; }
		}

		public bool IsEnabled
		{
			get { return _isEnabled; }
		}
	}
}

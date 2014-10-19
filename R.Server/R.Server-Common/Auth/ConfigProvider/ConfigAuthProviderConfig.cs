using System.Xml.Serialization;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	[XmlRoot(ConfigName, Namespace = ConfigNamespace)]
	public class ConfigAuthProviderConfig : IConfigAuthProviderConfig
	{
		public const string ConfigName = "config-auth";
		public const string ConfigNamespace = RServerConfig.ConfigNamespace;

		private string[] _allUserRoles = EmptyArray<string>.Value;
		private ConfigUser[] _credentials = EmptyArray<ConfigUser>.Value;
		private string[] _ownUserRoles = EmptyArray<string>.Value;

		[XmlElement("user", typeof (ConfigUser))]
		public ConfigUser[] Credentials
		{
			get { return _credentials; }
			set { _credentials = value; }
		}

		[XmlArray("own-users")]
		[XmlArrayItem("role", typeof (string))]
		public string[] OwnUserRoles
		{
			get { return _ownUserRoles; }
			set { _ownUserRoles = value ?? new string[0]; }
		}

		#region IConfigAuthProviderConfig Members
		[XmlArray("all-users")]
		[XmlArrayItem("role", typeof (string))]
		public string[] AllUserRoles
		{
			get { return _allUserRoles; }
			set { _allUserRoles = value ?? new string[0]; }
		}

		IConfigUser[] IConfigAuthProviderConfig.Users
		{
			get { return _credentials; }
		}
		#endregion
	}
}
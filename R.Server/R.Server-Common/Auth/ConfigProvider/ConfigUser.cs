using System.Xml.Serialization;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	public class ConfigUser : IConfigUser
	{
		private string[] _roles = EmptyArray<string>.Value;

		#region IConfigUser Members
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("provider")]
		public string Provider { get; set; }

		[XmlAttribute("password-hash")]
		public string PasswordHash { get; set; }

		[XmlElement("role", typeof (string))]
		public string[] Roles
		{
			get { return _roles; }
			set { _roles = value ?? EmptyArray<string>.Value; }
		}
		#endregion
	}
}
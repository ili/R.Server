using System.Xml.Serialization;

namespace R.Server.Common
{
	/// <summary>
	/// For serialization only.
	/// </summary>
	[XmlRoot(ConfigName, Namespace = ConfigNamespace)]
	public class WindowsAuthConfig : IWindowsAuthConfig
	{
		public const string ConfigName = "windows-auth";
		public const string ConfigNamespace = RServerConfig.ConfigNamespace;

		#region IWindowsAuthConfig Members
		[XmlElement("group")]
		public string Group { get; set; }
		#endregion
	}
}
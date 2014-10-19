using System.Xml.Serialization;

namespace R.Server.Common
{
	[XmlRoot(ConfigName, Namespace = ConfigNamespace)]
	public class NativeAuthConfig : INativeAuthConfig
	{
		public const string ConfigName = "native-auth";
		public const string ConfigNamespace = RServerConfig.ConfigNamespace;

		#region INativeAuthConfig Members
		[XmlElement("db-name")]
		public string DBName { get; set; }
		#endregion
	}
}
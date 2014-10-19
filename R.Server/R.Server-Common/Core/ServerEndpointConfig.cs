using System.Xml.Serialization;

namespace R.Server.Common
{
	/// <summary>
	/// For serialization only!
	/// </summary>
	public class ServerEndpointConfig : IServerEndpointConfig
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlElement("description")]
		public string Description { get; set; }

		[XmlAttribute("transport")]
		public string Transport { get; set; }

		[XmlAttribute("path")]
		public string Path { get; set; }

		[XmlAttribute("port")]
		public int Port { get; set; }
	}
}
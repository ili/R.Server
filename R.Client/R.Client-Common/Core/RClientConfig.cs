using System.Xml.Serialization;

namespace R.Client.Common
{
	/// <summary>
	/// For serialization only!
	/// </summary>
	[XmlRoot(ClientHost.ConfigName, Namespace = ClientHost.ConfigNamespace)]
	public class RClientConfig : IRClientConfig
	{
		private string[] _extensionAssemblies = new string[0];
		private string[] _extensionLayers = new string[0];

		#region IRClientConfig Members
		[XmlArray("extensions")]
		[XmlArrayItem("extension", typeof (string))]
		public string[] ExtensionAssemblies
		{
			get { return _extensionAssemblies; }
			set { _extensionAssemblies = value ?? new string[0]; }
		}

		[XmlArray("extension-layers")]
		[XmlArrayItem("layer", typeof (string))]
		public string[] ExtensionLayers
		{
			get { return _extensionLayers; }
			set { _extensionLayers = value ?? new string[0]; }
		}
		#endregion
	}
}

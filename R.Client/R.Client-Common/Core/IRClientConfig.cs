using Rsdn.SmartApp.Configuration;

namespace R.Client.Common
{
	[XmlSerializerSection(typeof (RClientConfig))]
	public interface IRClientConfig
	{
		string[] ExtensionAssemblies { get; }
		string[] ExtensionLayers { get; }
	}
}

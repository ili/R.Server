using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	[XmlSerializerSection(typeof (NativeAuthConfig),
		SchemaResource = RServerConfig.SchemaResourceName)]
	public interface INativeAuthConfig
	{
		string DBName { get; }
	}
}
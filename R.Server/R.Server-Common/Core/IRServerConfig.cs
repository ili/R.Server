using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	[XmlSerializerSection(
		typeof (RServerConfig),
		SchemaResource = RServerConfig.SchemaResourceName)]
	public interface IRServerConfig
	{
		string Name { get; }
		string Description { get; }
		IServerEndpointConfig[] EndpointConfigs { get; }
		string[] AuthenticationProviders { get; }
		string[] AuthorizationProviders { get; }
		string[] CommPolicies { get; }
		string[] ExtensionAssemblies { get; }
		string[] ExtensionLayers { get; }
	}
}
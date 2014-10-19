using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	[XmlSerializerSection(typeof (ConfigAuthProviderConfig),
		SchemaResource = RServerConfig.SchemaResourceName)]
	public interface IConfigAuthProviderConfig
	{
		string[] AllUserRoles { get; }
		string[] OwnUserRoles { get; }
		IConfigUser[] Users { get; }
	}
}
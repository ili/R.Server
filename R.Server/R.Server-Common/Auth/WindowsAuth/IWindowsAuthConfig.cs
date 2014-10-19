using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	[XmlSerializerSection(typeof (WindowsAuthConfig),
		SchemaResource = RServerConfig.SchemaResourceName)]
	public interface IWindowsAuthConfig
	{
		string Group { get; }
	}
}
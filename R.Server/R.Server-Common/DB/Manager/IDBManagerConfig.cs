using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	[XmlSerializerSection(typeof (DBManagerConfig),
		SchemaResource = RServerConfig.SchemaResourceName)]
	public interface IDBManagerConfig
	{
		IDBDescriptor[] DBDescriptors { get; }
	}
}
using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	[XmlSerializerSection(typeof (LoggerConfig),
		SchemaResource = "R.Server.Common.Logging.Config.LoggerConfig.xsd")]
	public interface ILoggerConfig
	{
		ILogTargetConfig[] Targets { get; }
	}
}
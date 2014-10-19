using System.Xml.Serialization;

using R.Common;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	/// <summary>
	/// For serialization only
	/// </summary>
	[XmlRoot(ConfigName, Namespace = ConfigNamespace)]
	public class LoggerConfig : ILoggerConfig
	{
		public const string ConfigName = "logger";
		public const string ConfigNamespace = CommonConstants.BaseXmlNamespace + "LoggerConfig.xsd";

		private LogTargetConfig[] _targets = EmptyArray<LogTargetConfig>.Value;

		[XmlElement("target", typeof (LogTargetConfig))]
		public LogTargetConfig[] Targets
		{
			get { return _targets; }
			set { _targets = value ?? EmptyArray<LogTargetConfig>.Value; }
		}

		ILogTargetConfig[] ILoggerConfig.Targets
		{
			get { return _targets; }
		}
	}
}
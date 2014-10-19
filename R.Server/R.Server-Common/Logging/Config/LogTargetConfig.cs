using System.Xml.Serialization;

namespace R.Server.Common
{
	/// <summary>
	/// For serialization only.
	/// </summary>
	public class LogTargetConfig : ILogTargetConfig
	{
		private TargetParameterConfig[] _parameters = new TargetParameterConfig[0];

		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlElement("param", typeof (TargetParameterConfig))]
		public TargetParameterConfig[] Parameters
		{
			get { return _parameters; }
			set { _parameters = value ?? new TargetParameterConfig[0]; }
		}

		ITargetParameterConfig[] ILogTargetConfig.Parameters
		{
			get { return Parameters; }
		}
	}
}
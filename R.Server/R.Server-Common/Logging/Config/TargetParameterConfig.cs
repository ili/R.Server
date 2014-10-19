using System.Xml.Serialization;

namespace R.Server.Common
{
	/// <summary>
	/// For serialization only.
	/// </summary>
	public class TargetParameterConfig : ITargetParameterConfig
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlText]
		public string Value { get; set; }
	}
}
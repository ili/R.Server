using System.Xml.Serialization;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	/// <summary>
	/// For serialization only.
	/// </summary>
	public abstract class ElementPolicyConfig
	{
		private string[] _allowRoles = EmptyArray<string>.Value;

		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlElement("allow", typeof (string))]
		public string[] AllowRoles
		{
			get { return _allowRoles; }
			set { _allowRoles = value ?? EmptyArray<string>.Value; }
		}
	}
}
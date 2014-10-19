using System.Xml.Serialization;

namespace R.Server.Common
{
	public class DBDescriptor : IDBDescriptor
	{
		#region IDBDescriptor Members
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("driver")]
		public string DriverName { get; set; }

		[XmlText]
		public string ConnectionString { get; set; }
		#endregion
	}
}
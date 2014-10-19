using System.Xml.Serialization;

namespace R.Server.Common
{
	[XmlRoot(ConfigName, Namespace = ConfigNamespace)]
	public class DBManagerConfig : IDBManagerConfig
	{
		public const string ConfigName = "db-manager";
		public const string ConfigNamespace = RServerConfig.ConfigNamespace;

		private DBDescriptor[] _dbDescriptors = new DBDescriptor[0];

		[XmlElement("db", typeof (DBDescriptor))]
		public DBDescriptor[] DBDescriptors
		{
			get { return _dbDescriptors; }
			set { _dbDescriptors = value ?? new DBDescriptor[0]; }
		}

		#region IDBManagerConfig Members
		IDBDescriptor[] IDBManagerConfig.DBDescriptors
		{
			get { return _dbDescriptors; }
		}
		#endregion
	}
}
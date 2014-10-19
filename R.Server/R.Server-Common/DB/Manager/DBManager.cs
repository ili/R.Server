using System.Linq;
#pragma warning disable 1692
using System;
using System.Collections.Generic;
using System.Data;

using R.Server.ServerModel;

using Rsdn.SmartApp;
using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	using DrvSvc = IRegKeyedElementsService<string, DBDriverInfo>;

	[Service(typeof(IDBManager))]
	public class DBManager : ServiceConsumer, IDBManager
	{
		private readonly IDictionary<string, IDBDescriptor> _descriptors;
		private readonly ElementsCache<string, IDBDriver> _drivers;

		[ExpectService]
		private IConfigService _cfgSvc;

		[ExpectService]
		private DrvSvc _drvSvc;

		public DBManager(IServiceProvider provider) : base(provider)
		{
			var config = _cfgSvc.GetSection<IDBManagerConfig>();

			_descriptors = 
				config
					.DBDescriptors
					.ToDictionary(
						desc =>
						{
							if (string.IsNullOrEmpty(desc.Name))
								throw new ArgumentNullException("provider");
							if (string.IsNullOrEmpty(desc.DriverName))
								throw new ArgumentNullException("provider");
							if (string.IsNullOrEmpty(desc.ConnectionString))
								throw new ArgumentNullException("provider");
							if (!_drvSvc.ContainsElement(desc.DriverName))
								throw new ArgumentException("Database driver '" + desc.DriverName + "' not registered.");
							return desc.Name;
						});

			_drivers = new ElementsCache<string, IDBDriver>(
				name => (IDBDriver)_drvSvc.GetElement(name).Type.CreateInstance(provider));
		}

		#region IDBManager Members
		public IDbConnection CreateConnection(string dbName)
		{
			IDBDescriptor desc;
			if (!_descriptors.TryGetValue(dbName, out desc))
				throw new ArgumentException("Database '" + dbName + "' does not exists.");
			return _drivers.Get(desc.DriverName).CreateConnection(desc.ConnectionString);
		}
		#endregion
	}
}

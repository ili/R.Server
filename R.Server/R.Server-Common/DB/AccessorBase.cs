using System;
using System.Data;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	internal class AccessorBase
	{
		private readonly string _dbName;
		private IDBManager _dbManager;
		private readonly object _dbMgrLockFlag = new object();

		protected AccessorBase(IServiceProvider provider, string dbName)
		{
			Provider = provider;
			_dbName = dbName;
		}

		private IServiceProvider Provider { get; set; }

		private IDBManager GetDBManager()
		{
			if (_dbManager == null)
				lock (_dbMgrLockFlag)
					if (_dbManager == null)
					{
						_dbManager = Provider.GetService<IDBManager>();
						if (_dbManager == null)
							throw new ServiceNotFoundException(typeof (IDBManager));
					}
			return _dbManager;
		}

		protected IDbConnection CreateConnection()
		{
			return GetDBManager().CreateConnection(_dbName);
		}
	}
}
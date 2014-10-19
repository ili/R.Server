using System.Data;

namespace R.Server.ServerModel
{
	public interface IDBManager
	{
		IDbConnection CreateConnection(string dbName);
	}
}
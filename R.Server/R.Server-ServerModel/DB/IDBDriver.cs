using System.Data;

namespace R.Server.ServerModel
{
	public interface IDBDriver
	{
		IDbConnection CreateConnection(string connectionString);
	}
}
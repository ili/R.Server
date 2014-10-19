using System.Data;
using System.Data.SqlClient;

using R.Server.ServerModel;

namespace R.Server.Common
{
	[DBDriver(DriverName)]
	public class MSSqlDBDriver : IDBDriver
	{
		public const string DriverName = "MSSql";

		#region IDBDriver Members
		public IDbConnection CreateConnection(string connectionString)
		{
			return new SqlConnection(connectionString);
		}
		#endregion
	}
}
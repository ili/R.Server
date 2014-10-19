using System.Data;

namespace R.Server.Common
{
	public static class DBHelper
	{
		public static IDbDataParameter AddParameter(this IDbCommand cmd, string name)
		{
			return AddParameter(cmd, name, null);
		}

		public static IDbDataParameter AddParameter(this IDbCommand cmd, string name, object value)
		{
			var p = cmd.CreateParameter();
			p.ParameterName = name;
			p.Value = value;
			cmd.Parameters.Add(p);

			return p;
		}
	}
}
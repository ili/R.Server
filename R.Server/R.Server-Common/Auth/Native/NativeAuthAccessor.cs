using System;
using System.Linq;

namespace R.Server.Common
{
	internal class NativeAuthAccessor : AccessorBase
	{
		public NativeAuthAccessor(IServiceProvider provider, string dbName) : base(provider, dbName)
		{
		}

		public byte[] GetPasswordHash(string userName)
		{
			using (var con = CreateConnection())
			using (var db = new AuthDataContext(con))
				return db.GetPasswordHash(userName);
		}

		public string[] GetLoginRoles(string authenType, string userName)
		{
			using (var con = CreateConnection())
			using (var db = new AuthDataContext(con))
				return db.GetLoginRoles(authenType, userName).Select(r => r.RoleName).ToArray();
		}
	}
}
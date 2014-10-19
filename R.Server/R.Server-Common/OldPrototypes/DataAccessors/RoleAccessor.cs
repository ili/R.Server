using System.Collections.Generic;
using System.Data;

using BLToolkit.Data;
using BLToolkit.DataAccess;

using R.Server.Common.ObjectModel;

namespace R.Server.Common.DataAccessors
{
	public abstract class RoleAccessor : PrimaryDataAccessor<Role>
	{
		public abstract int GetRoleCountByName(string @roleName);

		[ActionName("LoadAll")]
		protected abstract IDataReader LoadAllInternal(DbManager db);

		public virtual Dictionary<int,Role> LoadAll()
		{
			using (DbManager   db = GetDbManager())
			using (IDataReader dr = LoadAllInternal(db))
			{
				// Load roles.
				//
				Dictionary<int,Role> roles = 
					db.MappingSchema.MapDataReaderToDictionary<int,Role>(dr, "ID");

				// Load accounts.
				//
				dr.NextResult();

				List<AccountRole> ars = db.MappingSchema.MapDataReaderToList<AccountRole>(dr);

				foreach (AccountRole ar in ars)
					roles[ar.RoleID].Accounts.Add(ar.AccountID, ar);

				return roles;
			}
		}
	}
}
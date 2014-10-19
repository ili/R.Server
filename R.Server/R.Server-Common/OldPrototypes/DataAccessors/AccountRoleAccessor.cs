using BLToolkit.Data;

using R.Server.Common.ObjectModel;

using RoleType=R.Server.Common.ObjectModel.RoleType;

namespace R.Server.Common.DataAccessors
{
	public abstract class AccountRoleAccessor : PrimaryDataAccessor<AccountRole>
	{
		public abstract int  GetCountByAccountID           (int @accountID);
		public abstract int  GetCountByRoleID              (int @roleID);
		public abstract int  GetCountByAccountIDAndRoleType(DbManager db, int @accountID, RoleType @roleType);

		public abstract void DeleteByAccountID             (DbManager db, int @accountID);
	}
}
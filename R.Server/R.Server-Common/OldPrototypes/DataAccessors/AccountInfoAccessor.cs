using BLToolkit.Data;

using R.Server.Common.ObjectModel;

namespace R.Server.Common.DataAccessors
{
	public abstract class AccountInfoAccessor : PrimaryDataAccessor<AccountInfo>
	{
		public abstract void DeleteByAccountID(DbManager db, int @id);
		public abstract void InsertByType     (DbManager db, int @id, string @code, string @value);
	}
}
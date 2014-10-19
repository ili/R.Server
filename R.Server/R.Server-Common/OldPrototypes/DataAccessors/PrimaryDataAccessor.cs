using BLToolkit.Data;
using BLToolkit.DataAccess;

using R.Server.Common.Services;

namespace R.Server.Common.DataAccessors
{
	public abstract class PrimaryDataAccessor<T> : DataAccessor<T>
	{
		public override DbManager GetDbManager()
		{
			return DataAccessService.CreatePrimaryDbManager();
		}
	}
}
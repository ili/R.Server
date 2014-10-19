using BLToolkit.Data;
using BLToolkit.DataAccess;

using R.Server.Common.Services;

namespace R.Server.Common.DataAccessors
{
	public abstract class SupportDataAccessor<T> : DataAccessor<T>
	{
		public override DbManager GetDbManager()
		{
			return DataAccessService.CreateSupportDbManager();
		}
	}
}
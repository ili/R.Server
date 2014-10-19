using System.Configuration.Provider;

using BLToolkit.Data;
using BLToolkit.DataAccess;

using R.Server.Common.DataAccessors;

namespace R.Server.Common.Services
{
	public class DataAccessProvider : ProviderBase
	{
		public virtual DbManager CreatePrimaryDbManager() { return new DbManager("Primary"); }
		public virtual DbManager CreateSupportDbManager() { return new DbManager("Support"); }

		public virtual AccountAccessor CreateAccountAccessor()
		{
			return DataAccessor.CreateInstance<AccountAccessor>();
		}

		internal static AccountInfoAccessor CreateAccountInfoAccessor()
		{
			return DataAccessor.CreateInstance<AccountInfoAccessor>();
		}

		public AccountInfoTypeAccessor CreateAccountInfoTypeAccessor()
		{
			return DataAccessor.CreateInstance<AccountInfoTypeAccessor>();
		}

		internal static AccountActivityAccessor CreateAccountActivityAccessor()
		{
			return DataAccessor.CreateInstance<AccountActivityAccessor>();
		}

		internal static RoleAccessor CreateRoleAccessor()
		{
			return DataAccessor.CreateInstance<RoleAccessor>();
		}

		public AccountRoleAccessor CreateAccountRoleAccessor()
		{
			return DataAccessor.CreateInstance<AccountRoleAccessor>();
		}
	}
}
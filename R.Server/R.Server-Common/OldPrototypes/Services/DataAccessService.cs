using BLToolkit.Data;

using R.Server.Common.DataAccessors;

namespace R.Server.Common.Services
{
	public class DataAccessService : ServiceBase<DataAccessProvider>
	{
		static DataAccessService()
		{
			LoadProviders("r.server/dataAccessService");
		}

		public static DbManager       CreatePrimaryDbManager() { return Provider.CreatePrimaryDbManager(); }
		public static DbManager       CreateSupportDbManager() { return Provider.CreateSupportDbManager(); }
		public static AccountAccessor CreateAccountAccessor () { return Provider.CreateAccountAccessor();  }
		public static RoleAccessor CreateRoleAccessor() { return DataAccessProvider.CreateRoleAccessor(); }

		public static AccountInfoAccessor CreateAccountInfoAccessor()
		{
			return DataAccessProvider.CreateAccountInfoAccessor();
		}

		public static AccountInfoTypeAccessor CreateAccountInfoTypeAccessor()
		{
			return Provider.CreateAccountInfoTypeAccessor();
		}

		public static AccountActivityAccessor CreateAccountActivityAccessor()
		{
			return DataAccessProvider.CreateAccountActivityAccessor();
		}

		public static AccountRoleAccessor CreateAccountRoleAccessor()
		{
			return Provider.CreateAccountRoleAccessor();
		}
	}
}
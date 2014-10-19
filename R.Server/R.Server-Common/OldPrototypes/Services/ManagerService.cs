using R.Server.Common.BusinessLogic;
using R.Server.Common.Services;

namespace R.Server.Common.Services
{
	public class ManagerService : ServiceBase<ManagerProvider>
	{
		static ManagerService()
		{
			LoadProviders("r.server/managerService");
		}

		public static AccountManager CreateAccountManager()  { return Provider.CreateAccountManager();  }
		public static RoleManager    CreateRoleManager   ()  { return Provider.CreateRoleManager();     }
	}
}
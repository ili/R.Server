using System.Configuration.Provider;

using R.Server.Common.BusinessLogic;

namespace R.Server.Common.Services
{
	public class ManagerProvider : ProviderBase
	{
		public virtual AccountManager CreateAccountManager() { return new AccountManager(); }
		public virtual RoleManager    CreateRoleManager   () { return new RoleManager();    }
	}
}
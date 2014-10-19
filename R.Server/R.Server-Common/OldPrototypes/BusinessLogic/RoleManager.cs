using System;
using System.Collections.Generic;

using BLToolkit.Data;

using R.Server.Common.BusinessLogic;
using R.Server.Common.DataAccessors;
using R.Server.Common.ObjectModel;
using R.Server.Common.Services;

using RoleType=R.Server.Common.ObjectModel.RoleType;

namespace R.Server.Common.BusinessLogic
{
	public class RoleManager
	{
		#region Accessors

		private   RoleAccessor _roleAccessor;
		protected RoleAccessor  RoleAccessor
		{
			get { return _roleAccessor ?? (_roleAccessor = DataAccessService.CreateRoleAccessor()); }
		}

		private   AccountRoleAccessor _accountRoleAccessor;
		protected AccountRoleAccessor  AccountRoleAccessor
		{
			get
			{
				return _accountRoleAccessor ??
					(_accountRoleAccessor = DataAccessService.CreateAccountRoleAccessor());
			}
		}

		private   AccountAccessor _accountAccessor;
		protected AccountAccessor  AccountAccessor
		{
			get
			{
				return _accountAccessor ?? 
					(_accountAccessor = DataAccessService.CreateAccountAccessor());
			}
		}

		#endregion

		#region Managers

		private AccountManager _accountManager;
		public  AccountManager  AccountManager
		{
			get { return _accountManager ?? (_accountManager = ManagerService.CreateAccountManager()); }
		}

		#endregion

		#region Role Cache

		private static object _cacheLock = new object();

		protected virtual void EnsureLoadRoles()
		{
			if (_roleByID == null)
			{
				lock (_cacheLock)
				{
					if (_roleByID == null)
					{
						_roleByName = new Dictionary<string,Role>();

						Dictionary<int,Role> roles = RoleAccessor.LoadAll();

						foreach (Role r in roles.Values)
							_roleByName.Add(r.Name, r);

						_roleByID = roles;
					}
				}
			}
		}

		private static Dictionary<int,Role> _roleByID;
		protected      Dictionary<int,Role>  RoleByID
		{
			get
			{
				EnsureLoadRoles();
				return _roleByID;
			}
		}

		private static Dictionary<string,Role> _roleByName;
		public         Dictionary<string,Role>  RoleByName
		{
			get
			{
				EnsureLoadRoles();
				return _roleByName;
			}
		}

		public void ClearCache()
		{
			lock (_cacheLock)
			{
				_roleByID   = null;
				_roleByName = null;
			}
		}

		#endregion

		public virtual Role GetRoleByName(string roleName)
		{
			Role role;
			return RoleByName.TryGetValue(roleName, out role) ? role : null;
		}

		public virtual bool IsUserInRole(string username, string roleName)
		{
			AccountImpl acc = AccountManager.GetAccountByName(username);

			if (acc == null)
				return false;

			Role role;

			return RoleByName.TryGetValue(roleName, out role) && role.IsAccountInRole(acc.ID);
		}

		protected internal virtual void LoadRoles(AccountImpl acc)
		{
			if (acc.IsCustomRole)
			{
				foreach (Role role in RoleByID.Values)
					if (role.IsAccountInRole(acc.ID))
						acc.AddRole(role);
			}
			else
			{
				foreach (Role role in RoleByID.Values)
					if (role.RoleType == acc.RoleType)
						acc.AddRole(role);
			}
		}

		public virtual void CreateRole(string roleName)
		{
			Role role = new Role();

			role.Name     = roleName;
			role.RoleType = RoleType.Custom;

			//RoleAccessor.InsertSql(role);

			ClearCache();
			AccountManager.ClearCache();
		}

		public virtual bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			Role role = GetRoleByName(roleName);

			if (role == null)
				return false;

			if (throwOnPopulatedRole && AccountRoleAccessor.GetCountByRoleID(role.ID) > 0)
				throw new Exception("Role is not empty.");

			/*if (RoleAccessor.DeleteByKeySql(role.ID) > 0)
			{
				ClearCache();
				AccountManager.ClearCache();

				return true;
			}*/

			return false;
		}

		public virtual bool RoleExists(string name)
		{
			return RoleAccessor.GetRoleCountByName(name) > 0;
		}

		public virtual void AddRoles(AccountImpl acc, params string[] roleNames)
		{
			if (acc.IsAnonym)
				return;

			using (DbManager db = AccountAccessor.GetDbManager())
			{
				db.BeginTransaction();

				AccountRole ar = new AccountRole();

				ar.AccountID = acc.ID;

				foreach (string roleName in roleNames)
				{
					Role role = GetRoleByName(roleName);

					if (role != null || role.IsAccountInRole(acc.ID) == false)
					{
						ar.RoleID = role.ID;
						//AccountRoleAccessor.InsertSql(db, ar);
					}
				}

				if (acc.IsUser)
				{
					foreach (Role r in RoleByID.Values)
					{
						if (r.IsUser)
						{
							ar.RoleID = r.ID;
							//AccountRoleAccessor.InsertSql(db, ar);

							break;
						}
					}

					AccountAccessor.ChangeRoleType(db, acc.ID, RoleType.Custom);
				}

				db.CommitTransaction();
			}

			ClearCache();
			AccountManager.ClearCache();
		}

		public virtual void RemoveRoles(AccountImpl acc, string[] roleNames)
		{
			if (acc.IsAnonym)
				return;

			using (DbManager db = AccountAccessor.GetDbManager())
			{
				db.BeginTransaction();

				AccountRole ar = new AccountRole();

				ar.AccountID = acc.ID;

				foreach (string roleName in roleNames)
				{
					Role role = GetRoleByName(roleName);

					if (role != null || role.IsAccountInRole(acc.ID) == false)
					{
						ar.RoleID = role.ID;
						//AccountRoleAccessor.DeleteSql(db, ar);
					}
				}

				if (AccountRoleAccessor.GetCountByAccountIDAndRoleType(db, acc.ID, RoleType.Custom) == 0)
				{
					AccountRoleAccessor.DeleteByAccountID(db, acc.ID);
					AccountAccessor.    ChangeRoleType   (db, acc.ID, RoleType.User);
				}

				db.CommitTransaction();
			}

			ClearCache();
			AccountManager.ClearCache();
		}
	}
}
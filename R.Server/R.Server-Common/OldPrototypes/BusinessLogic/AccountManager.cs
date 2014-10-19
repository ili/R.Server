using System;
using System.Collections.Generic;

using BLToolkit.Data;
using BLToolkit.Validation;

using R.Server.Common.DataAccessors;
using R.Server.Common.ObjectModel;
using R.Server.Common.Services;

using PasswordFormat=R.Server.Common.ObjectModel.PasswordFormat;

namespace R.Server.Common.BusinessLogic
{
	public class AccountManager
	{
		#region Accessors

		private   AccountAccessor _accountAccessor;
		protected AccountAccessor  AccountAccessor
		{
			get
			{
				return _accountAccessor ??
					(_accountAccessor = DataAccessService.CreateAccountAccessor());
			}
		}

		private   AccountInfoAccessor _accountInfoAccessor;
		protected AccountInfoAccessor  AccountInfoAccessor
		{
			get
			{
				return _accountInfoAccessor ??
					(_accountInfoAccessor = DataAccessService.CreateAccountInfoAccessor());
			}
		}

		private   AccountInfoTypeAccessor _accountInfoTypeAccessor;
		protected AccountInfoTypeAccessor  AccountInfoTypeAccessor
		{
			get
			{
				return _accountInfoTypeAccessor ??
					(_accountInfoTypeAccessor = DataAccessService.CreateAccountInfoTypeAccessor());
			}
		}

		private   AccountActivityAccessor _accountActivityAccessor;
		protected AccountActivityAccessor  AccountActivityAccessor
		{
			get
			{
				return _accountActivityAccessor ??
					(_accountActivityAccessor = DataAccessService.CreateAccountActivityAccessor());
			}
		}

		#endregion

		#region Managers

		private RoleManager _roleManager;
		public  RoleManager  RoleManager
		{
			get { return _roleManager ?? (_roleManager = ManagerService.CreateRoleManager()); }
		}

		#endregion

		#region AccountImpl Cache

		private static object                     _cacheLock          = new object();
		private static Dictionary<int,   AccountImpl> _accountsByID       = new Dictionary<int,   AccountImpl>();
		private static Dictionary<string,AccountImpl> _accountsByGlobalID = new Dictionary<string,AccountImpl>();
		private static Dictionary<string,AccountImpl> _accountsByName     = new Dictionary<string,AccountImpl>();

		protected virtual AccountImpl GetAccountFromCache(int id)
		{
			AccountImpl accountImpl;
			return _accountsByID.TryGetValue(id, out accountImpl) ? accountImpl : null;
		}

		protected virtual AccountImpl GetAccountFromCache(object globalID)
		{
			AccountImpl accountImpl;
			return _accountsByGlobalID.TryGetValue(globalID.ToString(), out accountImpl) ? accountImpl : null;
		}

		protected virtual AccountImpl GetAccountFromCache(string loginName)
		{
			AccountImpl accountImpl;
			return _accountsByName.TryGetValue(loginName, out accountImpl) ? accountImpl : null;
		}

		protected virtual void AddAccountToCache(AccountImpl accountImpl)
		{
			_accountsByID      [accountImpl.ID]        = accountImpl;
			_accountsByGlobalID[accountImpl.GlobalID]  = accountImpl;
			_accountsByName    [accountImpl.LoginName] = accountImpl;
		}

		protected virtual void RemoveAccountFromCache(int id)
		{
			lock (_cacheLock)
			{
				AccountImpl accountImpl;

				if (_accountsByID.TryGetValue(id, out accountImpl))
				{
					_accountsByID.      Remove(accountImpl.ID);
					_accountsByGlobalID.Remove(accountImpl.GlobalID);
					_accountsByName.    Remove(accountImpl.LoginName);
				}
			}
		}

		protected internal void RemoveAccountFromCache(AccountImpl acc)
		{
			RemoveAccountFromCache(acc.ID);
		}

		public virtual void ClearCache()
		{
			lock (_cacheLock)
			{
				_accountsByID.      Clear();
				_accountsByGlobalID.Clear();
				_accountsByName.    Clear();
			}
		}

		#endregion

		#region GetAccount

		public virtual AccountImpl GetAccountByID(int id)
		{
			AccountImpl accountImpl = GetAccountFromCache(id);

			if (accountImpl == null)
			{
				lock (_cacheLock)
				{
					accountImpl = GetAccountFromCache(id);

					if (accountImpl == null)
					{
						accountImpl = AccountAccessor.SelectByID(id);

						if (accountImpl != null)
						{
							AddAccountToCache(accountImpl);
							RoleManager.LoadRoles(accountImpl);
						}
					}
				}
			}

			return accountImpl;
		}

		public virtual AccountImpl GetAccountByGlobalID(object globalID)
		{
			AccountImpl accountImpl = GetAccountFromCache(globalID);

			if (accountImpl == null)
			{
				lock (_cacheLock)
				{
					accountImpl = GetAccountFromCache(globalID);

					if (accountImpl == null)
					{
						accountImpl = AccountAccessor.SelectByGlobalID(globalID.ToString());

						if (accountImpl != null)
						{
							AddAccountToCache(accountImpl);
							RoleManager.LoadRoles(accountImpl);
						}
					}
				}
			}

			return accountImpl;
		}

		public virtual AccountImpl GetAccountByName(string loginName)
		{
			AccountImpl accountImpl = GetAccountFromCache(loginName);

			if (accountImpl == null)
			{
				lock (_cacheLock)
				{
					accountImpl = GetAccountFromCache(loginName);

					if (accountImpl == null)
					{
						accountImpl = AccountAccessor.SelectByName(loginName);

						if (accountImpl != null)
							AddAccountToCache(accountImpl);
					}
				}
			}

			return accountImpl;
		}

		public virtual AccountImpl GetAccountByEmail(string email)
		{
			return AccountAccessor.SelectByEmail(email);
		}

		#endregion

		#region GetAccountList

		public virtual List<AccountImpl> GetAccountList(int pageIndex, int pageSize, out int totalRecords)
		{
			return AccountAccessor.SelectPage(pageIndex, pageSize, out totalRecords);
		}

		public virtual List<AccountImpl> GetAccountListByEmail(
			string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			return AccountAccessor.SelectPageByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
		}

		public virtual List<AccountImpl> GetAccountListByName(
			string nameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			return AccountAccessor.SelectPageByName(nameToMatch, pageIndex, pageSize, out totalRecords);
		}

		#endregion

		#region AccountImpl

		public virtual void DeleteNotApprovedAccounts(DateTime notActivatedSinceDate)
		{
			try
			{
				AccountAccessor.DeleteNotApproved(notActivatedSinceDate);
			}
			catch
			{
			}
		}

		public virtual bool CheckNewLoginName(string username)
		{
			return AccountAccessor.GetCountByLoginName(username) == 0;
		}

		public virtual bool CheckNewEmail(string email)
		{
			return AccountAccessor.GetCountByEmail(email) == 0;
		}

		public virtual void CreateAccount(AccountImpl acc)
		{
			acc.Validate();
			acc.LastUpdateDate = DateTime.UtcNow;
			
			// AccountAccessor.InsertSql(acc);
		}

		public virtual void UpdateAccount(AccountImpl acc)
		{
			acc.Validate();
			acc.LastUpdateDate = DateTime.UtcNow;

			using (DbManager db = AccountAccessor.GetDbManager())
			{
				db.BeginTransaction();

				//AccountAccessor.    UpdateSql        (db, acc);
				AccountInfoAccessor.DeleteByAccountID(db, acc.ID);

				foreach (string code in acc.Values.Keys)
					AccountInfoAccessor.InsertByType(db, acc.ID, code, acc.Values[code]);

				db.CommitTransaction();
			}

			RemoveAccountFromCache(acc.ID);
		}

		public virtual void UnlockAccount(int id)
		{
			AccountAccessor.UnlockAccount(id);
			RemoveAccountFromCache(id);
		}

		public virtual void DeleteAccount(int id)
		{
			//AccountAccessor.DeleteByKeySql(id);
			RemoveAccountFromCache(id);
		}

		public virtual int DeleteInactiveAccount(DateTime inactiveSinceDate)
		{
			int count = 0;

			foreach (int id in AccountActivityAccessor.GetInactiveList(inactiveSinceDate))
			{
				try
				{
					//AccountAccessor.DeleteByKeySql(id);
					RemoveAccountFromCache(id);
					count++;
				}
				catch
				{
				}
			}

			return count;
		}

		#endregion

		#region AccountActivity

		public virtual void UpdateActivityDate(AccountImpl acc)
		{
			AccountActivityAccessor.UpdateActivityDate(acc.ID, acc.LastActivityDate = DateTime.UtcNow);
		}

		public virtual AccountActivity GetAccountActivity(int id)
		{
			return null;// AccountActivityAccessor.SelectByKey(id);
		}

		public virtual int GetActiveAccountCountSince(DateTime activeSinceDate)
		{
			return AccountActivityAccessor.GetActiveCountSince(activeSinceDate);
		}

		public virtual int GetInactiveAccountCountSince(DateTime inactiveSinceDate)
		{
			return AccountActivityAccessor.GetInactiveCountSince(inactiveSinceDate);
		}

		#endregion

		#region Password

		public virtual void ChangePassword(AccountImpl acc)
		{
			if (acc.PasswordFormat == PasswordFormat.Clear)
				acc.PasswordSalt = null;

			if (!acc.IsPasswordValid)
				throw new ValidationException("Invalid password.");

			AccountAccessor.ChangePassword(acc.ID, acc.Password, acc.PasswordSalt, acc.PasswordFormat);

			RemoveAccountFromCache(acc.ID);
		}

		public virtual void ChangePasswordQuestionAndAnswer(AccountImpl acc)
		{
			if (acc.AnswerFormat == PasswordFormat.Clear)
				acc.AnswerSalt = null;

			if (!acc.IsAnswerValid(true))
				throw new ValidationException("Invalid password answer.");

			if (!acc.IsQuestionValid(true))
				throw new ValidationException("Invalid password question");

			AccountAccessor.ChangePasswordQuestionAndAnswer(
				acc.ID, acc.PasswordQuestion, acc.PasswordAnswer, acc.AnswerSalt, acc.AnswerFormat);

			RemoveAccountFromCache(acc.ID);
		}

		#endregion

		#region Roles

		public List<AccountImpl> GetAccountListByRole(int roleID)
		{
			return AccountAccessor.SelectByRoleID(roleID);
		}

		public List<AccountImpl> GetAccountListByRole(int roleID, string nameToMatch)
		{
			return AccountAccessor.SelectByRoleIDAndName(roleID, nameToMatch);
		}

		#endregion
	}
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.Profile;

using R.Common.BusinessLogic;
using R.Common.ObjectModel;
using R.Common.Services;

namespace R.Web.Common.Providers
{
	public class RProfileProvider : ProfileProvider
	{
		#region Init

		public override void Initialize(string name, NameValueCollection config)
		{
			base.Initialize(name, config);

			_applicationName = config["applicationName"];
		}

		#endregion

		#region Protected Members

		private AccountManager _accountManager = ManagerService.CreateAccountManager();

		protected ProfileInfo Convert(Account account)
		{
			return new ProfileInfo(
				account.LoginName,
				false,
				account.LastActivityDate,
				account.LastUpdateDate,
				0);
		}

		#endregion

		#region Public Properties

		private         string _applicationName;
		public override string  ApplicationName
		{
			get { return _applicationName;  }
			set { _applicationName = value; }
		}

		#endregion

		#region Delete Profile

		public override int DeleteProfiles(ProfileInfoCollection profiles)
		{
			if (profiles == null) throw new ArgumentNullException("profiles");

			string[] names = new string[profiles.Count];

			int i = 0;

			foreach (ProfileInfo profile in profiles)
				names[i++] = profile.UserName;

			return DeleteProfiles(names);
		}

		public override int DeleteProfiles(string[] usernames)
		{
			int count = 0;

			foreach (string username in usernames)
			{
				Account acc = _accountManager.GetAccountByName(username);

				if (acc == null)
					continue;

				try
				{
					_accountManager.DeleteAccount(acc.ID);
					count++;
				}
				catch
				{
				}
			}

			return count;
		}

		public override int DeleteInactiveProfiles(
			ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			return (authenticationOption & ProfileAuthenticationOption.Authenticated) != 0?
				_accountManager.DeleteInactiveAccount(userInactiveSinceDate) : 0;
		}

		#endregion

		#region Inactive Profiles

		public override int GetNumberOfInactiveProfiles(
			ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			return (authenticationOption & ProfileAuthenticationOption.Authenticated) != 0?
				_accountManager.GetInactiveAccountCountSince(userInactiveSinceDate) : 0;
		}

		public override ProfileInfoCollection GetAllInactiveProfiles(
			ProfileAuthenticationOption authenticationOption,
			DateTime userInactiveSinceDate,
			int pageIndex,
			int pageSize,
			out int totalRecords)
		{
			throw new NotImplementedException("The method 'RProfileProvider.GetAllInactiveProfiles(...)' is not implemented.");
		}

		public override ProfileInfoCollection FindInactiveProfilesByUserName(
			ProfileAuthenticationOption authenticationOption,
			string usernameToMatch,
			DateTime userInactiveSinceDate,
			int pageIndex,
			int pageSize,
			out int totalRecords)
		{
			throw new NotImplementedException("The method 'RProfileProvider.FindInactiveProfilesByUserName(...)' is not implemented.");
		}

		#endregion

		#region Get Profiles
		
		public override ProfileInfoCollection GetAllProfiles(
			ProfileAuthenticationOption authenticationOption,
			int     pageIndex,
			int     pageSize,
			out int totalRecords)
		{
			totalRecords = 0;

			ProfileInfoCollection col = new ProfileInfoCollection();

			if ((authenticationOption & ProfileAuthenticationOption.Authenticated) != 0)
			{
				List<Account> list = _accountManager.GetAccountList(pageIndex, pageSize, out totalRecords);

				foreach (Account acc in list)
					col.Add(Convert(acc));
			}

			return col;
		}

		public override ProfileInfoCollection FindProfilesByUserName(
			ProfileAuthenticationOption authenticationOption,
			string  usernameToMatch,
			int     pageIndex,
			int     pageSize,
			out int totalRecords)
		{
			totalRecords = 0;

			ProfileInfoCollection col = new ProfileInfoCollection();

			if ((authenticationOption & ProfileAuthenticationOption.Authenticated) != 0)
			{
				List<Account> list = _accountManager.GetAccountListByName(
					usernameToMatch, pageIndex, pageSize, out totalRecords);

				foreach (Account acc in list)
					col.Add(Convert(acc));
			}

			return col;
		}

		#endregion

		#region Profile Values

		public override SettingsPropertyValueCollection GetPropertyValues(
			SettingsContext context, SettingsPropertyCollection collection)
		{
			SettingsPropertyValueCollection col = new SettingsPropertyValueCollection();

			return col;
		}

		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
		{
		}

		#endregion
	}
}

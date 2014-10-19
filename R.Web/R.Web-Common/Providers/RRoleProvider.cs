using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Web.Security;

using R.Common.BusinessLogic;
using R.Common.ObjectModel;
using R.Common.Services;

namespace R.Web.Common.Providers
{
	public class RRoleProvider : RoleProvider
	{
		#region Init

		public override void Initialize(string name, NameValueCollection config)
		{
			base.Initialize(name, config);

			_applicationName = config["applicationName"];
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

		#region Protected members

		private RoleManager _roleManager = ManagerService.CreateRoleManager();
		
		#endregion

		#region Roles & Users

		public override bool IsUserInRole(string username, string roleName)
		{
			return _roleManager.IsUserInRole(username, roleName);
		}

		public override string[] GetRolesForUser(string username)
		{
			Account acc = _roleManager.AccountManager.GetAccountByName(username);

			if (acc == null)
				return null;

			string[] roles = new string[acc.Roles.Count];

			for (int i = 0; i < acc.Roles.Count; i++)
				roles[i] = acc.Roles[i].Name;

			return roles;
		}

		public override string[] GetUsersInRole(string roleName)
		{
			Role role = _roleManager.GetRoleByName(roleName);

			if (role == null || !role.IsCustom)
				return null;

			List<Account> list = _roleManager.AccountManager.GetAccountListByRole(role.ID);

			string[] users = new string[list.Count];

			for (int i = 0; i < users.Length; i++)
				users[i] = list[i].LoginName;

			return users;
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			foreach (string username in usernames)
			{
				Account acc = _roleManager.AccountManager.GetAccountByName(username);

				if (acc != null)
					_roleManager.AddRoles(acc, roleNames);
			}
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			foreach (string username in usernames)
			{
				Account acc = _roleManager.AccountManager.GetAccountByName(username);

				if (acc != null)
					_roleManager.RemoveRoles(acc, roleNames);
			}
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			Role role = _roleManager.GetRoleByName(roleName);

			if (role == null)
				return null;

			List<Account> list =
				_roleManager.AccountManager.GetAccountListByRole(role.ID, usernameToMatch);

			string[] names = new string[list.Count];

			for (int i = 0; i < names.Length; i++)
				names[i] = list[i].LoginName;

			return names;
		}

		#endregion

		#region Roles

		public override string[] GetAllRoles()
		{
			List<string> roles = new List<string>();

			foreach (Role role in _roleManager.RoleByName.Values)
				if (role.IsCustom)
					roles.Add(role.Name);

			return roles.ToArray();
		}

		public override void CreateRole(string roleName)
		{
			_roleManager.CreateRole(roleName);
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			try
			{
				return _roleManager.DeleteRole(roleName, throwOnPopulatedRole);
			}
			catch (Exception ex)
			{
				if (throwOnPopulatedRole)
					throw new ProviderException(ex.Message, ex);

				throw;
			}
		}

		public override bool RoleExists(string roleName)
		{
			return _roleManager.RoleExists(roleName);
		}

		#endregion
	}
}

#pragma warning disable 1692
using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using R.Server.ServerModel;

using Rsdn.SmartApp;
using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	[AuthorizationProvider(ProviderName)]
	public class ConfigAuthorizationProvider : ServiceConsumer, IAuthorizationProvider
	{
		public const string ProviderName = "Config";

		private readonly Dictionary<LoginProviderPair, string[]> _roles =
			new Dictionary<LoginProviderPair, string[]>();

		private readonly string[] _allRoles;
		private readonly string[] _ownRoles;

#pragma warning disable ConvertToConstant
		[ExpectService]
		private IConfigService _cfgSvc;
#pragma warning restore ConvertToConstant

		public ConfigAuthorizationProvider(IServiceProvider provider) : base(provider)
		{
			var config = _cfgSvc.GetSection<IConfigAuthProviderConfig>();
			_allRoles = config.AllUserRoles;
			_ownRoles = config.OwnUserRoles;
			foreach (var user in config.Users)
				_roles.Add(
					new LoginProviderPair(
						user.Name,
						user.Provider.IsNullOrEmpty()
							? ConfigAuthenticationProvider.ProviderName
							: user.Provider), 
					user.Roles);
		}

		#region IAuthorizationProvider Members
		/// <summary>
		/// Authorize supplied identity.
		/// </summary>
		/// <returns>array of roles</returns>
		public string[] Authorize(IRServerIdentity identity)
		{
			var empty = EmptyArray<string>.Value;
			string[] roles;
			if (!_roles.TryGetValue(new LoginProviderPair(identity.Name, identity.AuthenticationType), out roles))
				roles = empty;
			var result = roles.Concat(_allRoles);
			if (identity.AuthenticationType == ConfigAuthenticationProvider.ProviderName)
				result = result.Concat(_ownRoles);
			return result.ToArray();
		}
		#endregion

		#region LoginProviderPair struct
		private struct LoginProviderPair
		{
			private readonly string _login;
			private readonly string _provider;

			public LoginProviderPair([NotNull] string login, [NotNull] string provider)
			{
				_login = login;
				_provider = provider;
			}

			private bool Equals(LoginProviderPair other)
			{
				return Equals(other._login, _login) && Equals(other._provider, _provider);
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj))
					return false;
				if (obj.GetType() != typeof (LoginProviderPair))
					return false;
				return Equals((LoginProviderPair)obj);
			}

			public override int GetHashCode()
			{
				unchecked
				{
					return (_login.GetHashCode()*397) ^ _provider.GetHashCode();
				}
			}
		}
		#endregion
	}
}
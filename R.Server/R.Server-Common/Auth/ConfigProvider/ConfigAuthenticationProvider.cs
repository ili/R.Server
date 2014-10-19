#pragma warning disable 1692
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using R.Server.ServerModel;

using Rsdn.SmartApp;
using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	[AuthenticationProvider(ProviderName)]
	public class ConfigAuthenticationProvider : ServiceConsumer, IAuthenticationProvider
	{
		public const string ProviderName = "Config";

		[ExpectService]
		private IConfigService _configService;

		private readonly IConfigAuthProviderConfig _config;

		private readonly Dictionary<string, string> _credentials =
			new Dictionary<string, string>();

		private readonly ElementsCache<UserNamePasswordPair, bool> _cache;
		private readonly HashAlgorithm _hashAlg = new SHA1Managed();

		public ConfigAuthenticationProvider(IServiceProvider provider) : base(provider)
		{
			_config = _configService.GetSection<IConfigAuthProviderConfig>();
			var ownUsers =
				_config
					.Users
					.Where(usr => !usr.PasswordHash.IsNullOrEmpty());
			foreach (var user in ownUsers)
				_credentials.Add(user.Name, user.PasswordHash);
			_cache = new ElementsCache<UserNamePasswordPair, bool>(DoAuthenticate);
		}

		private bool DoAuthenticate(UserNamePasswordPair pair)
		{
			string pwd;
			if (!_credentials.TryGetValue(pair.UserName, out pwd))
				return false;
			var pwdHash = Convert.ToBase64String(_hashAlg.ComputeHash(
				Encoding.UTF8.GetBytes(pair.Password)));
			return pwd == pwdHash;
		}

		#region IAuthenticationProvider Members
		public bool Authenticate(string userName, string password)
		{
			return _cache.Get(new UserNamePasswordPair(userName, password ?? ""));
		}
		#endregion
	}
}
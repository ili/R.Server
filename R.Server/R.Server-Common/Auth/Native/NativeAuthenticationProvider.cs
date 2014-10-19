#pragma warning disable 1692
using System;
using System.Security.Cryptography;
using System.Text;

using R.Server.ServerModel;

using Rsdn.SmartApp;
using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	[AuthenticationProvider(ProviderName)]
	public class NativeAuthenticationProvider : ServiceConsumer, IAuthenticationProvider
	{
		private const int _maxCacheSize = 10000;
		public const string ProviderName = "Native";

		private readonly LimitedElementsCache<Pair<string, string>, bool> _authenticatedCreds;
		private readonly INativeAuthConfig _config;
		private readonly HashAlgorithm _hashAlg = new SHA1Managed();
		private readonly NativeAuthAccessor _accessor;

		[ExpectService]
		private IConfigService _cfgSvc;

		public NativeAuthenticationProvider(IServiceProvider provider) : base(provider)
		{
			_config = _cfgSvc.GetSection<INativeAuthConfig>();
			_accessor = new NativeAuthAccessor(provider, _config.DBName);

			_authenticatedCreds = new LimitedElementsCache<Pair<string, string>, bool>(
				DoAuthenticate, _maxCacheSize);
		}

		private bool DoAuthenticate(Pair<string, string> key)
		{
			var pwdHash = _accessor.GetPasswordHash(key.First);
			if (pwdHash == null) // No user found
				return false;
			// Calc hash
			var credPwdHash = _hashAlg.ComputeHash(Encoding.UTF8.GetBytes(key.Second));
			return CompareHashes(pwdHash, credPwdHash);
		}

		private static bool CompareHashes(byte[] hash1, byte[] hash2)
		{
			if (hash1 == null || hash2 == null || hash1.Length != hash2.Length)
				return false;
			for (var i = 0; i < hash1.Length; i++)
				if (hash1[i] != hash2[i])
					return false;
			return true;
		}

		#region IAuthenticationProvider Members
		public bool Authenticate(string userName, string password)
		{
			return _authenticatedCreds.Get(new Pair<string, string>(userName, password));
		}
		#endregion
	}
}
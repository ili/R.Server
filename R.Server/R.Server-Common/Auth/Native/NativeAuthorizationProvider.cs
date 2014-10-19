#pragma warning disable 1692
using System;

using R.Server.ServerModel;

using Rsdn.SmartApp;
using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	[AuthorizationProvider(ProviderName)]
	public class NativeAuthorizationProvider : ServiceConsumer, IAuthorizationProvider
	{
		public const string ProviderName = "Native";

		private const int _cacheSize = 12000;

		private readonly NativeAuthAccessor _accessor;
		private readonly LimitedElementsCache<LoginDesc, string[]> _rolesCache;

		[ExpectService]
		private IConfigService _cfgSvc;

		public NativeAuthorizationProvider(IServiceProvider provider) : base(provider)
		{
			var dbName = _cfgSvc.GetSection<INativeAuthConfig>().DBName;

			_accessor = new NativeAuthAccessor(provider, dbName);
			_rolesCache = new LimitedElementsCache<LoginDesc, string[]>(
				LoadLoginRoles,
				_cacheSize);
		}

		private string[] LoadLoginRoles(LoginDesc desc)
		{
			return _accessor.GetLoginRoles(desc.AuthenType, desc.UserName);
		}

		#region IAuthorizationProvider Members
		/// <summary>
		/// Authorize supplied identity.
		/// </summary>
		/// <returns>array of roles</returns>
		public string[] Authorize(IRServerIdentity identity)
		{
			return _rolesCache.Get(
				new LoginDesc(identity.AuthenticationType, identity.Name));
		}
		#endregion

		#region LoginDesc class
		private struct LoginDesc : IEquatable<LoginDesc>
		{
			private readonly string _authenType;
			private readonly string _userName;

			public LoginDesc(string authenType, string userName)
			{
				_authenType = authenType;
				_userName = userName;
			}

			public string AuthenType
			{
				get { return _authenType; }
			}

			public string UserName
			{
				get { return _userName; }
			}

			public bool Equals(LoginDesc loginDesc)
			{
				return Equals(_authenType, loginDesc._authenType) && Equals(_userName, loginDesc._userName);
			}

			public override bool Equals(object obj)
			{
				if (!(obj is LoginDesc)) return false;
				return Equals((LoginDesc) obj);
			}

			public override int GetHashCode()
			{
				return _authenType.GetHashCode() + 29*_userName.GetHashCode();
			}
		}
		#endregion
	}
}
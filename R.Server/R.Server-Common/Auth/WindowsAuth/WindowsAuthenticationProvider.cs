#pragma warning disable 1692

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Principal;

using R.Server.ServerModel;

using Rsdn.SmartApp;
using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	[AuthenticationProvider(ProviderName)]
	public class WindowsAuthenticationProvider : ServiceConsumer, IAuthenticationProvider
	{
		public const string ProviderName = "Windows";

		private const int _cacheSize = 100;

		private readonly WindowsUserNameSecurityTokenAuthenticator _winAuthenticator =
			new WindowsUserNameSecurityTokenAuthenticator(true);

		[ExpectService]
		private IConfigService _cfgSvc;

		private readonly IWindowsAuthConfig _config;
		private readonly LimitedElementsCache<UserNamePasswordPair, bool> _cache;

		public WindowsAuthenticationProvider(IServiceProvider provider) : base(provider)
		{
			_config = _cfgSvc.GetSection<IWindowsAuthConfig>();
			if (string.IsNullOrEmpty(_config.Group))
				throw new ApplicationException(
					"Windows group for windows authentication not specified in configuration.");
			_cache = new LimitedElementsCache<UserNamePasswordPair, bool>(DoLogon, _cacheSize);
		}

		private bool DoLogon(UserNamePasswordPair pair)
		{
			try
			{
				var claims = _winAuthenticator.ValidateToken(
					new UserNameSecurityToken(pair.UserName, pair.Password));
				var ctx = new REvaluationContext();
				object state = null;
				claims[0].Evaluate(ctx, ref state);
				return ctx.IsInGroup(_config.Group);
			}
			catch (SecurityTokenException)
			{
				return false;
			}
		}

		#region IAuthenticationProvider Members
		public bool Authenticate(string userName, string password)
		{
			return _cache.Get(new UserNamePasswordPair(userName, password));
		}
		#endregion

		#region REvaluationContext class
		private class REvaluationContext : EvaluationContext
		{
			private readonly List<ClaimSet> _claimSets =
				new List<ClaimSet>();

			private readonly ReadOnlyCollection<ClaimSet> _claimSetsWrapper;

			private readonly Dictionary<string, object> _properties =
				new Dictionary<string, object>();

			public REvaluationContext()
			{
				_claimSetsWrapper = new ReadOnlyCollection<ClaimSet>(_claimSets);
			}

			public override void AddClaimSet(IAuthorizationPolicy policy, ClaimSet claimSet)
			{
				_claimSets.Add(claimSet);
			}

			public override ReadOnlyCollection<ClaimSet> ClaimSets
			{
				get { return _claimSetsWrapper; }
			}

			public override int Generation
			{
				get { return 0; }
			}

			public override IDictionary<string, object> Properties
			{
				get { return _properties; }
			}

			public override void RecordExpirationTime(DateTime expirationTime)
			{
			}

			public bool IsInGroup(string group)
			{
				return new WindowsPrincipal(
					(WindowsIdentity) ((IList<IIdentity>) _properties["Identities"])[0])
					.IsInRole(group);
			}
		}
		#endregion
	}
}
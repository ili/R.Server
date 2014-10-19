using System;
using System.IdentityModel.Tokens;
using System.Linq;
using System.ServiceModel;

using R.Server.ClientCommon;
using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	internal class RServerWcfAuthorizationMgr : ServiceAuthorizationManager
	{
		private readonly AccessChecker _accessChecker;

		private readonly IAuthManager _authManager;

		public RServerWcfAuthorizationMgr(IServiceProvider provider, AccessChecker accessChecker)
		{
			_accessChecker = accessChecker;
			if (provider == null)
				throw new ArgumentNullException("provider");

			_authManager = provider.GetService<IAuthManager>();
			if (_authManager == null)
				throw new ServiceNotFoundException(typeof (IAuthManager));
		}

		protected override bool CheckAccessCore(OperationContext operationContext)
		{
			var identity = Authenticate(operationContext);
			if (identity == null)
				return false;

			var actionUri = new Uri(operationContext.IncomingMessageHeaders.Action);
			var svcName = operationContext.Host.BaseAddresses[0].LocalPath.Substring(1);
			var methodName = actionUri.Segments[actionUri.Segments.Length - 1];

			var principal = Authorize(operationContext, identity, svcName, methodName);

			// Legacy principal-based security support
			operationContext
				.ServiceSecurityContext
				.AuthorizationContext
				.Properties["Principal"] = principal;

			// Do external comm access check if exists
			return _accessChecker == null
				|| _accessChecker(
					svcName,
					operationContext.Host.Description.ServiceType,
					methodName,
					principal);
		}

		private IRServerIdentity Authenticate(OperationContext context)
		{
			var tokens = context.SupportingTokens;
			var cfg =
				tokens == null
					? null
					: tokens.FirstOrDefault(c => c.SecurityToken is UserNameSecurityToken);
			if (cfg == null)
				throw new SecurityTokenException("Authentication token not found.");
			var authenToken = (UserNameSecurityToken) cfg.SecurityToken;
			var credentials = new RServerCredentials(authenToken.UserName, authenToken.Password);
			var identity = _authManager.Authenticate(credentials);
			return identity;
		}

		private IRServerPrincipal Authorize(OperationContext context, IRServerIdentity identity,
			string svcName, string methodName)
		{
			var principal = _authManager.Authorize(
				identity,
				svcName,
				context.Host.Description.ServiceType,
				methodName);
			if (principal == null)
				throw new ApplicationException("Authorization error. AuthManager return empty principal.");
			return principal;
		}
	}

	internal delegate bool AccessChecker(string serviceName, Type serviceType, string methodName,
		IRServerPrincipal principal);
}
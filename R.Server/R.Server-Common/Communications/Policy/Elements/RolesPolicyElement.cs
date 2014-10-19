using System;

using R.Server.ServerModel;

namespace R.Server.Common
{
	/// <summary>
	/// Role based policy implementation.
	/// </summary>
	public class RolesPolicyElement : ICommPolicyElement
	{
		private readonly string[] _roles;

		public RolesPolicyElement(string[] roles)
		{
			_roles = roles;
		}

		#region IAuthPolicy Members
		/// <summary>
		/// Define supplied principal permission.
		/// </summary>
		public bool IsPermitted(
			IServiceProvider provider,
			IRServerPrincipal principal,
			CommPolicyContext context)
		{
			foreach (var role in _roles)
				if (principal.IsInRole(role))
					return true;
			return false;
		}
		#endregion
	}
}
using System;

using R.Server.ServerModel;

namespace R.Server.Common
{
	/// <summary>
	/// Policy attribute that based on role list.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	public abstract class RoleBasedPolicyAttribute : Attribute, ICommPolicyElement
	{
		protected abstract string[] GetRoles(IServiceProvider provider, IRServerPrincipal principal);

		#region IAuthPolicy Members
		/// <summary>
		/// Define supplied principal permission.
		/// </summary>
		public bool IsPermitted(IServiceProvider provider, IRServerPrincipal principal,
			CommPolicyContext context)
		{
			foreach (var role in GetRoles(provider, principal))
				if (principal.IsInRole(role))
					return true;
			return false;
		}
		#endregion
	}
}
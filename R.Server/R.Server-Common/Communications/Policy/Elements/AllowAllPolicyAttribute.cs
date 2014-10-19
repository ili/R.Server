using System;

using R.Server.ServerModel;

namespace R.Server.Common
{
	/// <summary>
	/// Policy attribute that deny nothing.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class AllowAllPolicyAttribute : Attribute, ICommPolicyElement
	{
		#region IAuthPolicy Members
		/// <summary>
		/// Define supplied principal permission.
		/// </summary>
		public bool IsPermitted(IServiceProvider provider, IRServerPrincipal principal,
			CommPolicyContext context)
		{
			return true;
		}
		#endregion
	}
}
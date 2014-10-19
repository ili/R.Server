using System;

using R.Server.ServerModel;

namespace R.Server.Common
{
	internal class AllowAllPolicyElement : ICommPolicyElement
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
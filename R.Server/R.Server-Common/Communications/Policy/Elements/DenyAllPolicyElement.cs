using System;

using R.Server.ServerModel;

namespace R.Server.Common
{
	internal class DenyAllPolicyElement : ICommPolicyElement
	{
		#region IAuthPolicy Members
		/// <summary>
		/// Define supplied principal permission.
		/// </summary>
		public bool IsPermitted(IServiceProvider provider, IRServerPrincipal principal,
			CommPolicyContext context)
		{
			return false;
		}
		#endregion
	}
}
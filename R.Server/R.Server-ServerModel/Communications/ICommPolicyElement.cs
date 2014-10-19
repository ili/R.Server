using System;

namespace R.Server.ServerModel
{
	/// <summary>
	/// Authorization policy.
	/// </summary>
	public interface ICommPolicyElement
	{
		/// <summary>
		/// Define supplied principal permission.
		/// </summary>
		bool IsPermitted(
			IServiceProvider provider,
			IRServerPrincipal principal,
			CommPolicyContext context);
	}
}
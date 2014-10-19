namespace R.Server.ServerModel
{
	public interface IAuthorizationProvider
	{
		/// <summary>
		/// Authorize supplied identity.
		/// </summary>
		/// <returns>array of roles</returns>
		string[] Authorize(IRServerIdentity identity);
	}
}
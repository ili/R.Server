namespace R.Server.ServerModel
{
	public interface IAuthenticationProvider
	{
		bool Authenticate(string userName, string password);
	}
}
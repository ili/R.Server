namespace R.Server.ServerModel
{
	public interface IRServerCredentials
	{
		string AuthenticationType { get; }

		string Login { get; }

		string Password { get; }

		string User { get; }
	}
}
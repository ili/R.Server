namespace R.Server.Common
{
	public interface IConfigUser
	{
		string Name { get; }
		string Provider { get; }
		string PasswordHash { get; }
		string[] Roles { get; }
	}
}
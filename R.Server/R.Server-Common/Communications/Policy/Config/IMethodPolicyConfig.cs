namespace R.Server.Common
{
	public interface IMethodPolicyConfig
	{
		string Name { get; }
		string[] AllowRoles { get; }
	}
}
namespace R.Server.Common
{
	public interface IServicePolicyConfig
	{
		string Name { get; }
		string[] AllowRoles { get; }
		IMethodPolicyConfig[] MethodPolicies { get; }
	}
}
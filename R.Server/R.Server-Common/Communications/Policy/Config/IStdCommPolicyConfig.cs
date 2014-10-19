using Rsdn.SmartApp.Configuration;

namespace R.Server.Common
{
	[XmlSerializerSection(typeof (StdCommPolicyConfig),
		AllowMerge = true,
		SchemaResource = RServerConfig.SchemaResourceName)]
	public interface IStdCommPolicyConfig
	{
		string[] AllowRoles { get; }
		IServicePolicyConfig[] ServicePolicies { get; }
	}
}
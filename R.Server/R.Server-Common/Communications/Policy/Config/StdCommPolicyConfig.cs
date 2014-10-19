using System.Xml.Serialization;

namespace R.Server.Common
{
	/// <summary>
	/// For serialization only.
	/// </summary>
	[XmlRoot(ConfigName, Namespace = ConfigNamespace)]
	public class StdCommPolicyConfig : IStdCommPolicyConfig
	{
		public const string ConfigName = "service-policy";
		public const string ConfigNamespace = RServerConfig.ConfigNamespace;

		private ServicePolicyConfig[] _servicePolicies = new ServicePolicyConfig[0];
		private string[] _allowRoles = new string[0];

		[XmlArray("server")]
		[XmlArrayItem("allow", typeof (string))]
		public string[] AllowRoles
		{
			get { return _allowRoles; }
			set { _allowRoles = value ?? new string[0]; }
		}

		[XmlElement("service", typeof (ServicePolicyConfig))]
		public ServicePolicyConfig[] ServicePolicies
		{
			get { return _servicePolicies; }
			set { _servicePolicies = value ?? new ServicePolicyConfig[0]; }
		}

		IServicePolicyConfig[] IStdCommPolicyConfig.ServicePolicies
		{
			get { return ServicePolicies; }
		}
	}
}
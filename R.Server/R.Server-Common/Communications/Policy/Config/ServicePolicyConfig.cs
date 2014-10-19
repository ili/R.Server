using System.Xml.Serialization;

namespace R.Server.Common
{
	/// <summary>
	/// For serialization only.
	/// </summary>
	public class ServicePolicyConfig : ElementPolicyConfig, IServicePolicyConfig
	{
		private MethodPolicyConfig[] _methodPolicies = new MethodPolicyConfig[0];

		[XmlElement("method", typeof (MethodPolicyConfig))]
		public MethodPolicyConfig[] MethodPolicies
		{
			get { return _methodPolicies; }
			set { _methodPolicies = value ?? new MethodPolicyConfig[0]; }
		}

		IMethodPolicyConfig[] IServicePolicyConfig.MethodPolicies
		{
			get { return MethodPolicies; }
		}
	}
}
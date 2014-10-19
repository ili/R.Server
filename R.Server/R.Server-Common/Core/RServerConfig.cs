using System.Xml.Serialization;

using R.Common;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	[XmlRoot(ConfigName, Namespace = ConfigNamespace)]
	public class RServerConfig : IRServerConfig
	{
		public const string ConfigName = "rserver";
		public const string ConfigNamespace = CommonConstants.BaseXmlNamespace + "RServerConfig.xsd";
		public const string SchemaResourceName = "R.Server.Common.RServerConfig.xsd";

		private string[] _authorizationProviders = EmptyArray<string>.Value;
		private string[] _authenticationProviders = EmptyArray<string>.Value;
		private string[] _commPolicies = EmptyArray<string>.Value;
		private string[] _extensionAssemblies = EmptyArray<string>.Value;
		private string[] _extensionLayers = EmptyArray<string>.Value;
		private ServerEndpointConfig[] _endpointConfigs = EmptyArray<ServerEndpointConfig>.Value;

		public RServerConfig()
		{
			Name = "";
			Description = "";
			BaseAddress = "";
		}

		[XmlElement("endpoint", typeof (ServerEndpointConfig))]
		public ServerEndpointConfig[] EndpointConfigs
		{
			get { return _endpointConfigs; }
			set { _endpointConfigs = value ?? EmptyArray<ServerEndpointConfig>.Value; }
		}

		#region IRServerConfig Members
		[XmlElement("base-address")]
		public string BaseAddress { get; set; }

		[XmlElement("use-authentication")]
		public string[] AuthenticationProviders
		{
			get { return _authenticationProviders; }
			set { _authenticationProviders = value ?? EmptyArray<string>.Value; }
		}

		[XmlElement("use-authorization")]
		public string[] AuthorizationProviders
		{
			get { return _authorizationProviders; }
			set { _authorizationProviders = value ?? EmptyArray<string>.Value; }
		}

		[XmlElement("comm-policy", typeof (string))]
		public string[] CommPolicies
		{
			get { return _commPolicies; }
			set { _commPolicies = value ?? EmptyArray<string>.Value; }
		}

		[XmlElement("description")]
		public string Description { get; set; }

		[XmlArray("extensions")]
		[XmlArrayItem("extension", typeof (string))]
		public string[] ExtensionAssemblies
		{
			get { return _extensionAssemblies; }
			set { _extensionAssemblies = value ?? EmptyArray<string>.Value; }
		}

		[XmlArray("extension-layers")]
		[XmlArrayItem("layer", typeof (string))]
		public string[] ExtensionLayers
		{
			get { return _extensionLayers; }
			set { _extensionLayers = value ?? EmptyArray<string>.Value; }
		}

		IServerEndpointConfig[] IRServerConfig.EndpointConfigs
		{
			get { return EndpointConfigs; }
		}

		[XmlElement("name")]
		public string Name { get; set; }
		#endregion
	}
}
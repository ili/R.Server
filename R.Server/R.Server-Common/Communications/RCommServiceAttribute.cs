using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	public class RCommServiceAttribute : CommServiceAttribute, IServiceBehavior, ICommPolicyElement
	{
		private readonly ServiceBehaviorAttribute _stdAttribute = new ServiceBehaviorAttribute();

		public RCommServiceAttribute(string name, Type contractType,
			InstanceContextMode instanceContextMode)
			: base(name, contractType)
		{
			_stdAttribute.InstanceContextMode = instanceContextMode;
		}

		public InstanceContextMode InstanceContextMode
		{
			get { return _stdAttribute.InstanceContextMode; }
		}

		#region IServiceBehavior Members
		///<summary>
		///Provides the ability to inspect the service host and the service description to confirm that the service can run successfully.
		///</summary>
		///
		///<param name="serviceHostBase">The service host that is currently being constructed.</param>
		///<param name="serviceDescription">The service description.</param>
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			((IServiceBehavior) _stdAttribute).Validate(serviceDescription, serviceHostBase);
		}

		///<summary>
		///Provides the ability to pass custom data to binding elements to support the contract implementation.
		///</summary>
		///
		///<param name="serviceHostBase">The host of the service.</param>
		///<param name="bindingParameters">Custom objects to which binding elements have access.</param>
		///<param name="serviceDescription">The service description of the service.</param>
		///<param name="endpoints">The service endpoints.</param>
		public void AddBindingParameters(ServiceDescription serviceDescription,
			ServiceHostBase serviceHostBase,
			Collection<ServiceEndpoint> endpoints,
			BindingParameterCollection bindingParameters)
		{
			((IServiceBehavior) _stdAttribute).AddBindingParameters(serviceDescription, serviceHostBase,
				endpoints, bindingParameters);
		}

		///<summary>
		///Provides the ability to change run-time property values or insert custom extension objects such as error handlers, message or parameter interceptors, security extensions, and other custom extension objects.
		///</summary>
		///
		///<param name="serviceHostBase">The host that is currently being built.</param>
		///<param name="serviceDescription">The service description.</param>
		public void ApplyDispatchBehavior(ServiceDescription serviceDescription,
			ServiceHostBase serviceHostBase)
		{
			var sme = serviceHostBase.Extensions.Find<ServiceProviderExtension>();
			if (sme == null)
				throw new ApplicationException("Could not find ServiceProviderExtension for service, "
					+ "that use RCommServiceAttribute");
			var instProv = sme.Provider.GetRequiredService<IInstanceProvider>();

			// Assign our instance provider to Dispatch behavior in each 
			// endpoint.
			foreach (var cdb in serviceHostBase.ChannelDispatchers)
			{
				var cnlDisp = cdb as ChannelDispatcher;
				if (cnlDisp != null)
					foreach (var epDisp in cnlDisp.Endpoints)
						epDisp.DispatchRuntime.InstanceProvider = instProv;
			}

			((IServiceBehavior) _stdAttribute).ApplyDispatchBehavior(serviceDescription, serviceHostBase);
		}
		#endregion

		#region ICommPolicyElement Members
		/// <summary>
		/// Define supplied principal permission.
		/// </summary>
		public bool IsPermitted(IServiceProvider provider, IRServerPrincipal principal,
			CommPolicyContext context)
		{
			return provider.GetService<ConfigStdPolicyManager>().IsPermitted(principal, context);
		}
		#endregion
	}
}
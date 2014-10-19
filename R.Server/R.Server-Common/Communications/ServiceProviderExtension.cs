using System;
using System.ServiceModel;

namespace R.Server.Common
{
	/// <summary>
	/// Extension for supply service behavior by <see cref="IServiceProvider"/>
	/// </summary>
	internal class ServiceProviderExtension : IExtension<ServiceHostBase>
	{
		public ServiceProviderExtension(IServiceProvider provider)
		{
			Provider = provider;
		}

		public IServiceProvider Provider { get; private set; }

		#region IExtension<ServiceHostBase> Members
		///<summary>
		///Enables an extension object to find out when it has been aggregated. Called when the extension is added to the <see cref="P:System.ServiceModel.IExtensibleObject`1.Extensions"></see> property.
		///</summary>
		///
		///<param name="owner">The extensible object that aggregates this extension.</param>
		public void Attach(ServiceHostBase owner)
		{
		}

		///<summary>
		///Enables an object to find out when it is no longer aggregated. Called when an extension is removed from the <see cref="P:System.ServiceModel.IExtensibleObject`1.Extensions"></see> property.
		///</summary>
		///
		///<param name="owner">The extensible object that aggregates this extension.</param>
		public void Detach(ServiceHostBase owner)
		{
		}
		#endregion
	}
}
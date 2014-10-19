using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	internal class CommSvcInstanceProvider : IInstanceProvider
	{
		private readonly IServiceProvider _provider;

		public CommSvcInstanceProvider(IServiceProvider provider)
		{
			_provider = provider;
		}

		#region IInstanceProvider Members
		///<summary>
		///Returns a service object given the specified <see cref="T:System.ServiceModel.InstanceContext"></see> object.
		///</summary>
		///
		///<returns>
		///A user-defined service object.
		///</returns>
		///
		///<param name="instanceContext">The current <see cref="T:System.ServiceModel.InstanceContext"></see> object.</param>
		public object GetInstance(InstanceContext instanceContext)
		{
			return GetInstance(instanceContext, null);
		}

		///<summary>
		///Returns a service object given the specified <see cref="T:System.ServiceModel.InstanceContext"></see> object.
		///</summary>
		///
		///<returns>
		///The service object.
		///</returns>
		///
		///<param name="message">The message that triggered the creation of a service object.</param>
		///<param name="instanceContext">The current <see cref="T:System.ServiceModel.InstanceContext"></see> object.</param>
		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			var svcType = instanceContext.Host.Description.ServiceType;
			return svcType.CreateInstance(_provider);
		}

		///<summary>
		///Called when an <see cref="T:System.ServiceModel.InstanceContext"></see> object recycles a service object.
		///</summary>
		///
		///<param name="instanceContext">The service's instance context.</param>
		///<param name="instance">The service object to be recycled.</param>
		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
			var disp = instance as IDisposable;
			if (disp != null)
				disp.Dispose();
		}
		#endregion
	}
}
using System;
using System.ServiceModel;

using R.Server.ClientModel;
using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace R.Server.Common
{
	[RCommService(
		_serviceName,
		typeof (IRServerInfoService),
		InstanceContextMode.Single)]
	internal class RServerInfoService : IRServerInfoService
	{
		private const string _serviceName = "RServerInfo";

		private readonly IServiceProvider _provider;

		public RServerInfoService(IServiceProvider provider)
		{
			_provider = provider;
		}

		#region IRServerInfoService Members
		public RServerInfo GetInfo()
		{
			var rSrv = _provider.GetService<IRServer>();
			if (rSrv == null)
				throw new ServiceNotFoundException(typeof (IRServer));
			return new RServerInfo(rSrv.Name, rSrv.Description);
		}
		#endregion
	}
}
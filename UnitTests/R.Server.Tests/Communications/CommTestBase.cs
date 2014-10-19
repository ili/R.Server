using R.Server.ClientCommon;
using R.Server.ClientModel;
using R.Server.Common;

using Rsdn.SmartApp;

namespace Tests.Communications
{
	public class CommTestBase : RServerTestBase
	{
		private readonly ServiceManager _svcMgr;
		private readonly ProxyManager _proxyMgr;

		protected CommTestBase()
		{
			_svcMgr = new ServiceManager();
			var extMgr = new ExtensionManager(_svcMgr);
			extMgr.Scan(typeof (CommTransportRegStrategy).Assembly.GetTypes(),
				new CommTransportRegStrategy(_svcMgr));
			_svcMgr.Publish<ICommTransportManager>(new CommTransportManager(_svcMgr));
			_proxyMgr = new ProxyManager(
				_svcMgr,
				new RServerCredentials("Config", "TestAdmin", "apwd"),
				"http",
				"localhost",
				8000);
		}

		protected ProxyManager ProxyManager
		{
			get { return _proxyMgr; }
		}
	}
}
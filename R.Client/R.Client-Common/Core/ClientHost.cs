using System;
using System.Collections.Generic;
using System.Reflection;

using R.Client.Model;
using R.Common;

using Rsdn.SmartApp;
using Rsdn.SmartApp.Configuration;

namespace R.Client.Common
{
	using AppsSvc = IRegKeyedElementsService<string, ClientApplicationInfo>;

	/// <summary>
	/// Standard client host implementation.
	/// </summary>
	public class ClientHost : IClientHost, IDisposable
	{
		public const string ConfigName = "client-config";
		public const string ConfigNamespace = CommonConstants.BaseXmlNamespace + "ClientConfig.xsd";

		private readonly string _technology;
		private readonly ServiceManager _serviceManager;
		private IRClientConfig _config;
		private readonly List<Assembly> _loadedExtensions = new List<Assembly>();
		private readonly ElementsCache<string, IClientApplication> _applications;
		private readonly UIManagerService _uiManager;
		private readonly AppsSvc _appsSvc;

		public ClientHost(IServiceProvider provider, string technology, string configFile)
		{
			_technology = technology;
			_serviceManager = new ServiceManager(true, provider);
			_serviceManager.Publish<IClientHost>(this);

			var extMgr = RunExtensionManager();
			InitConfig(configFile);
			InitExtensions(extMgr);

			_uiManager = new UIManagerService(_serviceManager, _technology, _config.ExtensionLayers);
			_serviceManager.Publish<IUIManagerService>(_uiManager);

			_appsSvc = _serviceManager.GetService<AppsSvc>();
			_applications = new ElementsCache<string, IClientApplication>(
				appName =>
				{
					var info = _appsSvc.GetElement(appName);
					return (IClientApplication) info.AppType.CreateInstance(_serviceManager);
				});
		}		

		#region IClientHost Members
		public string[] GetApplicationNames()
		{
			return Array.ConvertAll(
				_appsSvc.GetRegisteredElements(),
				info => info.Name);
		}

		public IClientApplication GetApplication(string name)
		{
			return _applications.Get(name);
		}

		public string Technology
		{
			get { return _technology; }
		}

        public Assembly[] LoadedExtensions
        {
            get { return _loadedExtensions.ToArray(); }
        }

        public void Run()
        {
        }

        public void Stop()
        {
        }
		#endregion

		private void InitExtensions(IExtensionManager extMgr)
		{
			_config =
				_serviceManager
					.GetRequiredService<IConfigService>()
					.GetSection<IRClientConfig>();
			var asmHelper = new AssemblyScanHelper();
			foreach (var ext in _config.ExtensionAssemblies)
			{
				var asm = Assembly.Load(ext);
				_loadedExtensions.Add(asm);
				asmHelper.AddAssembly(asm);
			}
			extMgr.Scan(asmHelper.GetTypes(), new StrategyFactoryStrategy(_serviceManager));
			StrategyFactoryStrategy.ScanWithAllFactories(_serviceManager, extMgr, asmHelper.GetTypes());
		}

		private ExtensionManager RunExtensionManager()
		{
			var extMgr = new ExtensionManager(_serviceManager);
			var asmHelper = new AssemblyScanHelper();
			asmHelper.AddAssembly(GetType());
			StrategyFactoryStrategy.RegisterAndScan(_serviceManager, extMgr, asmHelper.GetTypes());
			return extMgr;
		}

		private void InitConfig(string configFile)
		{
			var cfgSvc =
				_serviceManager
					.CreateConfigService(
						configFile,
						new Dictionary<string, string>());
			_serviceManager.Publish(cfgSvc);
		}

		#region IServiceProvider Members
		/// <summary>
		/// Возвращает сервис, реализующий интерфейс T
		/// </summary>
		public object GetService(Type svcType)
		{
			return _serviceManager.GetService(svcType);
		}
		#endregion

		#region IDisposable Members
		///<summary>
		///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		///</summary>
		///<filterpriority>2</filterpriority>
		public void Dispose()
		{
			Stop();
		}
		#endregion
	}
}

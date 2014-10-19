using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using JetBrains.Annotations;
using R.Server.ServerModel;

using Rsdn.SmartApp;
using Rsdn.SmartApp.Configuration;

using EventHandler = Rsdn.SmartApp.EventHandler<R.Server.ServerModel.IRServer>;

namespace R.Server.Common
{
	public class RServer : IRServer, IDisposable
	{
		private readonly IConfigDataProvider _configDataProvider;
		private const string _logSource = "RServer";

		private readonly EventWaitHandle _runEvent = new EventWaitHandle(false,
			EventResetMode.AutoReset);

		private readonly ServiceManager _serviceManager;
		private LogHelper _logHelper;
		private IRServerConfig _config;
		private readonly List<Assembly> _loadedExtensions = new List<Assembly>();
		private CommServicesHost _commSvcsHost;

		public RServer(
			[CanBeNull] IServiceProvider provider,
			[NotNull] IConfigDataProvider configDataProvider,
			[NotNull] string workingDir)
		{
			if (configDataProvider == null)
				throw new ArgumentNullException("configDataProvider");
			if (workingDir.IsNullOrEmpty())
				throw new ArgumentNullException("workingDir");

			_configDataProvider = configDataProvider;
			WorkingDirectory = workingDir;
			_serviceManager =
				provider != null
					? new ServiceManager(true, provider)
					: new ServiceManager(true);
			_serviceManager.Publish<IRServer>(this);

			var extMgr = new ExtensionManager(_serviceManager);
			InitExtensions(extMgr);
			InitServices();
		}

		public RServer(
			IServiceProvider provider,
			string configFile,
			string workingDir)
			: this(provider, new FileConfigDataProvider(configFile), workingDir)
		{}

		private void InitConfig()
		{
			var cfgSvc = _serviceManager.CreateConfigService(
				_configDataProvider,
				new Dictionary<string, string>());
			_serviceManager.Publish<IConfigService>(cfgSvc);
		}

		private void ConfigChanged(IConfigService svc)
		{
			var hostSvc = _serviceManager.GetService<IRServerHost>();
			if (hostSvc != null)
			{
				_logHelper.LogInfo("Sending restart request due to configuration changes",
					LogEventImpotance.VeryImportant);
				hostSvc.Restart("Configuration file changed");
			}
			else
				_logHelper.LogWarning("Configuration changed. Manual restart required.",
					LogEventImpotance.VeryImportant);
		}

		private void InitExtensions(IExtensionManager extMgr)
		{
			var extCfgSvc = 
				new ConfigService(
					new[] { typeof (IRServerConfig) }.GetSectionInfos(),
					_configDataProvider,
					new Dictionary<string, string>());
			var asmHelper = new AssemblyScanHelper();
			asmHelper.AddAssembly(typeof(RServer));
			// CommTransportRegStrategy extension assembly hardcoded, because it is required for server core
			asmHelper.AddAssembly(typeof (CommTransportRegStrategy));
			var extAsms =
				extCfgSvc
					.GetSection<IRServerConfig>()
					.ExtensionAssemblies
					.Select(ext => Assembly.Load(ext));
			foreach (var asm in extAsms)
			{
				_loadedExtensions.Add(asm);
				asmHelper.AddAssembly(asm);
			}

			extMgr.Scan(new ConfigSectionStrategy(_serviceManager), asmHelper.GetTypes());
			InitConfig();
			_config =
				_serviceManager
					.GetRequiredService<IConfigService>()
					.GetSection<IRServerConfig>();

			extMgr.Scan(new StrategyFactoryStrategy(_serviceManager), asmHelper.GetTypes());
			StrategyFactoryStrategy
				.ScanWithAllFactories(
					_serviceManager,
					extMgr,
					asmHelper.GetTypes());
		}

		private void InitServices()
		{
			_logHelper = new LogHelper(_serviceManager, _logSource);

			_commSvcsHost = new CommServicesHost(_serviceManager, _config.EndpointConfigs,
				_config.CommPolicies);
			_serviceManager.Publish<ICommServicesHost>(_commSvcsHost);

			// Enforce scheduler creation
			// TODO: Must be replaced by event broker, when it ready
			_serviceManager.GetService<IScheduler>();
		}

		#region IRServer Members
		public string Name
		{
			get { return _config.Name; }
		}

		public string Description
		{
			get { return _config.Description; }
		}

		public string WorkingDirectory { get; private set; }

		public Assembly[] LoadedExtensions
		{
			get { return _loadedExtensions.ToArray(); }
		}

		public event EventHandler Started;
		public event EventHandler Stopped;
		public event EventHandler Disposing;

		public void Run(ServerStartedDelegate serverStartedDelegate)
		{
			using (_commSvcsHost)
			{
				_commSvcsHost.Run();

				if (Started != null)
					Started(this);

				_logHelper.LogInfo("RServer '" + Name + "' turn into running state.");
				Thread.Sleep(100); // Wait logger
				if (serverStartedDelegate != null)
					serverStartedDelegate();

				_serviceManager.GetRequiredService<IConfigService>().ConfigChanged += ConfigChanged;
				_runEvent.WaitOne();
				_serviceManager.GetRequiredService<IConfigService>().ConfigChanged -= ConfigChanged;
			}

			if (Stopped != null)
				Stopped(this);

			_logHelper.LogInfo("RServer turn into stopped state.");
		}

		public void Stop()
		{
			_runEvent.Set();
		}
		#endregion

		#region IDisposable Members
		///<summary>
		///Performs application-defined tasks associated with freeing, releasing,
		/// or resetting unmanaged resources.
		///</summary>
		public void Dispose()
		{
			Stop();

			if (Disposing != null)
				Disposing(this);

			_serviceManager.Dispose();
		}
		#endregion
	}
}
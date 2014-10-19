using R.Client.Model;
using R.Common;

using Rsdn.SmartApp;

using IServiceProvider=Rsdn.SmartApp.IServiceProvider;

namespace R.Server.ManagerApp
{
	[ClientApplication(AppName, AppDisplayName, AppDescription)]
	public class ManagerApp : IClientApplication
	{
		public const string AppName = "RServerManagerApp";
		public const string AppDisplayName = "R.Server Manager";
		public const string AppDescription = "R.Server management application";

		private readonly AppEntryPoint _serversEntry = new AppEntryPoint(
			"ServersEntry", "Add/Remove Servers …", true);
		private readonly ElementsCache<object, IUIController> _controllers;
		private ServersFormModel _serversModel;

		public ManagerApp(IServiceProvider provider)
		{
			IUIManagerService uiMgr = provider.GetService<IUIManagerService>();
			if (uiMgr == null)
				throw new ServiceNotFoundException(typeof (IUIManagerService));
			_controllers = new ElementsCache<object, IUIController>(
				delegate(object model)
				{
					return uiMgr.CreateControllerForModel(model);
				});
		}

		#region IClientApplication Members
		public object CreateAppModel()
		{
			return new MainFormModel();
		}

		public AppEntryPoint[] GetEntryPoints()
		{
			return new AppEntryPoint[] {_serversEntry};
		}

		public void ExecuteEntryPoint(string name)
		{
			if (name == _serversEntry.Name)
			{
				if (_serversModel == null)
					_serversModel = new ServersFormModel();
				_controllers.Get(_serversModel).Show();
			}
		}
		#endregion
	}
}

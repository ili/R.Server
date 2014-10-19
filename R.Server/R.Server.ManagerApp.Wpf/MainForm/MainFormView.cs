using R.Client.Common;
using R.Client.Model;
using R.Client.Wpf_Common;

using Rsdn.SmartApp;

namespace R.Server.ManagerApp.Wpf
{
	[View(typeof (IMainFormView), WellKnownTechnologies.Wpf, "")]
	public class MainFormView : WpfWindowView<MainWindow, MainFormModel>, IMainFormView
	{
		public MainFormView(IServiceProvider provider) : base(provider)
		{
		}
	}
}

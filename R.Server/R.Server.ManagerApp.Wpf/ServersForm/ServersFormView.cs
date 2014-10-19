using R.Client.Common;
using R.Client.Model;
using R.Client.Wpf_Common;

using Rsdn.SmartApp;

namespace R.Server.ManagerApp.Wpf
{
	[View(typeof (IServersFormView), WellKnownTechnologies.Wpf, "")]
	public class ServersFormView
		: WpfWindowView<ServersFormWindow, ServersFormModel>, IServersFormView
	{
		public ServersFormView(IServiceProvider provider) : base(provider)
		{
		}
	}
}

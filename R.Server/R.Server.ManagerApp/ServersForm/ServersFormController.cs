using R.Client.Common;
using R.Client.Model;

using Rsdn.SmartApp;

namespace R.Server.ManagerApp
{
	[UIControllerAttrbute(typeof (ServersFormModel), "")]
	public class ServersFormController : SingleViewController<IServersFormView>
	{
		public ServersFormController(IServiceProvider provider, ServersFormModel model)
			: base(provider)
		{
		}
	}
}

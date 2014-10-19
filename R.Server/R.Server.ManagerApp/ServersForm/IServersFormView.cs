using R.Client.Model;

namespace R.Server.ManagerApp
{
	public interface IServersFormView : IView
	{
		ServersFormModel Model
		{ get; set; }
	}
}

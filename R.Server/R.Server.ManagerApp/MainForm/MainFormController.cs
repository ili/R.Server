using R.Client.Common;
using R.Client.Model;

using Rsdn.SmartApp;

namespace R.Server.ManagerApp
{
	/// <summary>
	/// Default UI controller for <see cref="MainFormModel"/>
	/// </summary>
	[UIControllerAttrbute(typeof (MainFormModel), "")]
	public class MainFormController : SingleViewController<IMainFormView>
	{
		private readonly MainFormModel _model;

		public MainFormController(IServiceProvider provider, MainFormModel model) : base(provider)
		{
			_model = model;
		}
	}
}

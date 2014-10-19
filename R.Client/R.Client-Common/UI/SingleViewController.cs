using System;

using R.Client.Model;
using Rsdn.SmartApp;

namespace R.Client.Common
{
	/// <summary>
	/// UI controller with single view.
	/// </summary>
	public abstract class SingleViewController<V>
		: IUIController
		where V : IView
	{
		private readonly IServiceProvider _provider;
		private readonly V _view;

		protected SingleViewController(IServiceProvider provider)
		{
			if (provider == null)
				throw new ArgumentNullException("provider");
			_provider = provider;
			var uiMgr = _provider.GetService<IUIManagerService>();
			if (uiMgr == null)
				throw new ServiceNotFoundException(typeof (IUIManagerService));
			_view = uiMgr.CreateView<V>();
		}

		protected IServiceProvider Provider
		{
			get { return _provider; }
		}

		protected V View
		{
			get { return _view; }
		}

		#region IUIController Members
		public void Show()
		{
			_view.Show();
		}

		public void Hide()
		{
			_view.Hide();
		}

		public bool ShowDialog()
		{
			return _view.ShowDialog();
		}

		public event EventHandler Close
		{
			add { _view.Close += value; }
			remove { _view.Close -= value; }
		}
		#endregion
	}
}

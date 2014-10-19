using System;
using System.Windows;

using R.Client.Model;

using IServiceProvider=Rsdn.SmartApp.IServiceProvider;

namespace R.Client.Wpf_Common
{
	public class WpfWindowView<W, M> : IView where W : Window, new()
	{
		private readonly IServiceProvider _provider;
		private readonly W _window;

		public WpfWindowView(IServiceProvider provider)
		{
			_provider = provider;
			_window = new W();
		}

		public IServiceProvider Provider
		{
			get { return _provider; }
		}

		public W Window
		{
			get { return _window; }
		}

		public M Model
		{
			get { return (M)Window.DataContext; }
			set { Window.DataContext = value; }
		}

		#region IView Members
		public void Show()
		{
			_window.Show();
		}

		public void Hide()
		{
			_window.Hide();
		}

		public bool ShowDialog()
		{
			return _window.ShowDialog() == true;
		}

		public event EventHandler Close
		{
			add { _window.Closed += value; }
			remove { _window.Closed -= value; }
		}
		#endregion
	}
}

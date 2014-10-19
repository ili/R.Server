using System.Windows;
using System.Windows.Input;

using Rsdn.SmartApp;

namespace R.Client.WpfHost
{
	partial class MainContextMenuResource
	{
		private readonly IServiceProvider _provider;

		public MainContextMenuResource(IServiceProvider provider)
		{
			_provider = provider;
		}

		private void AppExitExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void AboutExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			new AboutWindow(_provider).ShowDialog();
		}
	}
}

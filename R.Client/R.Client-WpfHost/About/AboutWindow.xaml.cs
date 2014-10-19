using System;
using System.Windows.Input;

using R.Client.WpfHost.About;

using IServiceProvider=Rsdn.SmartApp.IServiceProvider;

namespace R.Client.WpfHost
{
	/// <summary>
	/// Interaction logic for AboutWindow.xaml
	/// </summary>
	public partial class AboutWindow
	{
		public AboutWindow(IServiceProvider provider)
		{
			InitializeComponent();

			DataContext = new AboutWindowModel(provider);
		}

		private void CloseExecuted(Object sender, ExecutedRoutedEventArgs e)
		{
			Close();
		}
	}
}
using System.Windows;

namespace R.Server.ManagerApp.Wpf
{
	/// <summary>
	/// Interaction logic for ServersFormWindow.xaml
	/// </summary>

	public partial class ServersFormWindow : Window
	{

		public ServersFormWindow()
		{
			InitializeComponent();
		}

		private void CloseExecuted(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
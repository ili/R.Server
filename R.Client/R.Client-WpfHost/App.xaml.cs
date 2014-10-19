using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

using R.Client.Common;
using R.Client.Model;

using Rsdn.SmartApp;

using ContextMenu=System.Windows.Controls.ContextMenu;
using MenuItem=System.Windows.Controls.MenuItem;
using MouseEventArgs=System.Windows.Forms.MouseEventArgs;
using MouseEventHandler=System.Windows.Forms.MouseEventHandler;

namespace R.Client.WpfHost
{
	using AppsSvc = IRegKeyedElementsService<string, ClientApplicationInfo>;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		public static readonly RoutedCommand AboutCommand = new RoutedCommand();
		public const string ConfigFile = "RClientConfig.xml";

		private readonly ClientHost _clientHost;
		private readonly NotifyIcon _notifyIcon;

		public App()
		{
			ServiceManager rootMgr = new ServiceManager(true);
			string[] args = Environment.GetCommandLineArgs();
			string cfgFile;
			if (args.Length > 1)
				cfgFile = new FileInfo(args[1]).FullName;
			else
				cfgFile = Path.Combine(Environment.CurrentDirectory, ConfigFile);
			_clientHost = new ClientHost(rootMgr, WellKnownTechnologies.Wpf, cfgFile);
			_notifyIcon = new NotifyIcon();
			_notifyIcon.Icon = new Icon(GetType().Assembly.GetManifestResourceStream(
				"R.Client.WpfHost.R.Icon-Inverse.ico"));
			ShutdownMode = ShutdownMode.OnExplicitShutdown;
		}

		///<summary>
		///Raises the <see cref="E:System.Windows.Application.Startup"></see> event.
		///</summary>
		///
		///<param name="e">A <see cref="T:System.Windows.StartupEventArgs"></see>
		/// that contains the event data.</param>
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			_clientHost.Run();
			RunAgent();
		}

		private void AppCommandExecuted(ClientApplicationInfo info, AppEntryPoint entry)
		{
			_clientHost.GetApplication(info.Name).ExecuteEntryPoint(entry.Name);
		}

		private void RunAgent()
		{
			_notifyIcon.Visible = true;
			_notifyIcon.MouseClick += new MouseEventHandler(NotifyIconMouseClick);
		}

		private void NotifyIconMouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				CreateContextMenu().IsOpen = true;
			}
		}

		private ContextMenu CreateContextMenu()
		{
			MainContextMenuResource cmRes = new MainContextMenuResource(_clientHost);
			cmRes.InitializeComponent();
			ContextMenu contextMenu = (ContextMenu)cmRes["MainContextMenu"];
			foreach (ClientApplicationInfo info in _clientHost.GetService<AppsSvc>().GetRegisteredElements())
			{
				AppEntryPoint[] entries = _clientHost.GetApplication(info.Name).GetEntryPoints();
				if (entries.Length > 0)
				{
					int place = 0;
					TextSeparator header = new TextSeparator();
					header.Text = info.DisplayName;
					contextMenu.Items.Insert(place, header);
					place++;
					foreach (AppEntryPoint entry in entries)
					{
						MenuItem mi = new MenuItem();
						mi.Header = entry.DisplayName;
						mi.IsEnabled = entry.IsEnabled;
						mi.Click += delegate { AppCommandExecuted(info, entry); };
						contextMenu.Items.Insert(place, mi);
						place++;
					}
				}
			}
			return contextMenu;
		}

		///<summary>
		///Raises the <see cref="E:System.Windows.Application.Exit"></see> event.
		///</summary>
		///
		///<param name="e">An <see cref="T:System.Windows.ExitEventArgs"></see> that contains the event data.</param>
		protected override void OnExit(ExitEventArgs e)
		{
			_clientHost.Dispose();
			_notifyIcon.Dispose();
			base.OnExit(e);
		}
	}
}
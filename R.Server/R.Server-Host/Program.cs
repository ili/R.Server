using System;
using System.IO;
using System.Threading;

using R.Common;
using R.Server.Common;
using R.Server.ServerModel;

using Rsdn.SmartApp;

namespace Rsdn.Server
{
	internal class Program : IRServerHost
	{
		private volatile bool _restartMode;
		private RServer _rServer;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Write2Console(CommonConstants.GetWelcomeString(HostConstants.ApplicationName));
			new Program().ConsoleRun();
		}

		private static void Write2Console(string message)
		{
			var oldColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Host: " + message);
			Console.ForegroundColor = oldColor;
		}

		private void ConsoleRun()
		{
			Write2Console("Console mode");
			Run();
		}

		private void Run()
		{
			var rootMgr = new ServiceManager();
			rootMgr.Publish<IRServerHost>(this);

			var wrkDir = Path.GetDirectoryName(new Uri(typeof (Program).Assembly.CodeBase).LocalPath);
			var cfgFile = Path.Combine(wrkDir, "RServerConfig.xml");

			Console.CancelKeyPress +=
				( sender, e ) =>
					{
						if (_rServer != null)
							_rServer.Stop();
						if (e.SpecialKey != ConsoleSpecialKey.ControlBreak)
							e.Cancel = true;
					};

			while (true)
				using (_rServer = new RServer(rootMgr, cfgFile, wrkDir))
				{
					_restartMode = false;
					_rServer.Run(( ) => Write2Console("Press [Ctrl-C] to exit"));
					if (_restartMode)
					{
						Thread.Sleep(200); // Prevent too fast restart
						GC.Collect();
						GC.WaitForPendingFinalizers();
						GC.Collect();
					}
					else
						break;
				}
		}

		#region IRServerHost Members
		public void Restart(string reason)
		{
			Write2Console("Restarting server. Reason: " + reason);
			_restartMode = true;
			_rServer.Stop();
		}
		#endregion
	}
}
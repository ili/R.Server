using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

using R.Server.ClientCommon;
using R.Server.ClientModel;
using R.Server.Common;

using Rsdn.SmartApp;
using Rsdn.TrafficTester;

namespace TrafficTester_Client
{
	internal static class Program
	{
		private static ProxyManager _proxyMgr;

		static void Main()
		{
			var svcMgr = new ServiceManager();
			var extMgr = new ExtensionManager(svcMgr);
			extMgr.Scan(typeof(CommTransportRegStrategy).Assembly.GetTypes(),
				new CommTransportRegStrategy(svcMgr));
			svcMgr.Publish<ICommTransportManager>(new CommTransportManager(svcMgr));

			_proxyMgr = new ProxyManager(
				svcMgr,
				new RServerCredentials("Config", "TestAdmin", "apwd"),
				"tcp",
				"localhost", // server address
				8001);

			var dlThread = new Thread(
				() => DoTest(
					1000, // poll interval
					"dl.log", // log file name
					prx => prx.DownloadPackage(16384/*package data size, 16K max*/).CheckHash()))
				{IsBackground = true};
			dlThread.Start();

			var ulThread = new Thread(
				() => DoTest(
					1000, // poll interval
					"ul.log", // log file name
					prx => prx.UploadPackage(TrafficTestHelper.CreateTestPackage(16384/*package data size, 16K max*/))))
					{ IsBackground = true };
			ulThread.Start();

			Console.WriteLine("Test started");
			Console.Read();
		}

		private static void LogMessage(string logName, string message)
		{
			File.AppendAllText(logName, "[{0}] {1}\r\n".FormatStr(DateTime.Now.ToString("hh:mm:ss"), message));
		}

		private static void DoTest(
			int interval,
			string logName,
			Func<ITrafficTestServiceProxy, bool> action)
		{
			while (true)
			{
				try
				{
					using (var proxy = _proxyMgr.CreateProxy<ITrafficTestServiceProxy>(TrafficTestHelper.ServiceName))
					{
						var sw = Stopwatch.StartNew();
						var res = action(proxy);
						sw.Stop();
						LogMessage(logName, "{0}. {1} ms".FormatStr(res ? "OK" : "ERR", sw.ElapsedMilliseconds));
					}
				}
				catch (Exception ex)
				{
					LogMessage(logName, ex.Message);
				}
				Thread.Sleep(interval);
			}
		}
	}
}

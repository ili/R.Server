using System;

namespace Rsdn.SmartApp.Demos
{
	internal static class Program
	{
		private static void Main()
		{
			ExtensionManager extMgr = new ExtensionManager(null);
			Type[] types = typeof (Program).Assembly.GetTypes();
			extMgr.Scan(types, new SimpleExtensionStrategy(Console.Out));

			Console.Read();
		}
	}
}

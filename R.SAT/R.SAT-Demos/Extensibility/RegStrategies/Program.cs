using System;

namespace Rsdn.SmartApp.Demos
{
	using FruitSvc = IRegElementsService<FruitInfo>;
	using KeyedFruitSvc = IRegKeyedElementsService<string, FruitInfo>;

	internal class Program
	{
		static void Main()
		{
			var extMgr = new ExtensionManager(null);
			var types = typeof(Program).Assembly.GetTypes();

			// Elements registration
			var regSvcMgr = new ServiceManager();
			extMgr.Scan(types, new RegFruitStrategy(regSvcMgr));
			var fruitSvc = regSvcMgr.GetService<FruitSvc>();
			foreach (var fi in fruitSvc.GetRegisteredElements())
				Console.WriteLine("{0}({1})", fi.Name, fi.Type);

			// Keyed elements registration
			var keyedRegSvcMgr = new ServiceManager();
			extMgr.Scan(types, new RegKeyedFruitStrategy(keyedRegSvcMgr));
			var keyedFruitSvc = keyedRegSvcMgr.GetService<KeyedFruitSvc>();
			Console.WriteLine("lemon type is '{0}'",
				keyedFruitSvc.GetElement("lemon").Type);

			Console.Read();
		}
	}
}

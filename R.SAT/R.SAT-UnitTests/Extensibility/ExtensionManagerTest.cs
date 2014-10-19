using System;
using System.Reflection;

using NUnit.Framework;

using Rsdn.SmartApp.Extensibility.Registration;

namespace Rsdn.SmartApp.Extensibility
{
	using INamedSvc = IRegKeyedElementsService<string, TestNamedElementInfo>;

	[TestFixture]
	public class ExtensionManagerTest
	{
		#region Setup/Teardown
		[SetUp]
		protected void SetUp()
		{
			_svcManager = new ServiceManager(true);
			_extManager = new ExtensionManager(_svcManager);
		}
		#endregion

		private ServiceManager _svcManager;
		private ExtensionManager _extManager;

		[Test]
		public void AssemblyScanning()
		{
			_extManager.Scan(Assembly.GetExecutingAssembly().GetTypes(),
				new SimpleExtensionStrategy());
			Assert.AreEqual(SimpleExtensionStrategy.LastExtensionType,
				typeof (SimpleExtension));
		}

		[Test]
		public void DisableAllScan()
		{
			_extManager.Scan(Assembly.GetExecutingAssembly().GetTypes(),
				new NamedElementStrategy(_svcManager));
			var svc = _extManager.GetService<INamedSvc>();
			Assert.IsFalse(svc.ContainsElement(DisableAllExtension.Name));
		}

		[Test]
		public void DisableScanByParamType()
		{
			_extManager.Scan(Assembly.GetExecutingAssembly().GetTypes(),
				new NamedElementStrategy(_svcManager));
			var svc = _extManager.GetService<INamedSvc>();
			Assert.IsFalse(svc.ContainsElement(DisableByParamTypeExtension.Name),
				"Disabled element registered");
			Assert.IsTrue(svc.ContainsElement(DisableByParamTypeExtension.Name2));
		}

		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void NullStrategy()
		{
			_extManager.Scan(new Type[0], null);
		}

		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void NullTypes()
		{
			_extManager.Scan(null, null);
		}

		[Test]
		public void SelfPublishing()
		{
			Assert.AreEqual(_extManager.GetService<IExtensionManager>(), _extManager);
		}
	}
}
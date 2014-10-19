using System;
using System.Reflection;

using NUnit.Framework;

namespace Rsdn.SmartApp.Extensibility.Registration
{
	using INamedSvc = IRegKeyedElementsService<string, TestNamedElementInfo>;
	using NamedSvc = RegKeyedElementsService<string, TestNamedElementInfo>;

	[TestFixture]
	public class RegistrationTest
	{
		#region Setup/Teardown
		[SetUp]
		protected void SetUp()
		{
			_svcManager = new ServiceManager();
			_extManager = new ExtensionManager(_svcManager);
		}
		#endregion

		private ServiceManager _svcManager;
		private ExtensionManager _extManager;

		private static void TestRegElementsInService<EI>(IRegElementsService<EI> svc)
			where EI : ElementInfo
		{
			var elems = svc.GetRegisteredElements();
			Assert.IsNotNull(elems);
			EI sampleElem = null;
			foreach (var elem in elems)
				if (elem.ElementType == typeof (SampleElement))
				{
					sampleElem = elem;
					break;
				}
			Assert.IsNotNull(sampleElem);
		}

		private void TestRegElements<EI>()
			where EI : ElementInfo
		{
			var svc = _extManager.GetService<IRegElementsService<EI>>();
			Assert.IsNotNull(svc);
			TestRegElementsInService(svc);
		}

		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void NamedSvcContainsParamIsNull()
		{
			var svc = new NamedSvc();
			svc.ContainsElement(null);
		}

		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void NamedSvcGetParamIsNull()
		{
			var svc = new NamedSvc();
			svc.GetElement(null);
		}

		[Test]
		public void RegElementsHelper()
		{
			_extManager.Scan(Assembly.GetExecutingAssembly().GetTypes(),
				new ElementStrategy(_svcManager));
			TestRegElements<ElementInfo>();
		}

		[Test]
		public void RegElementsService()
		{
			var svc = new RegElementsService<ElementInfo>();
			svc.Register(new ElementInfo(typeof (SampleElement)));
			TestRegElementsInService(svc);
		}

		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void RegElementsServiceInfoIsNull()
		{
			var svc = new RegElementsService<ElementInfo>();
			svc.Register(null);
		}

		[Test]
		public void RegNamedElementsHelper()
		{
			_extManager.Scan(Assembly.GetExecutingAssembly().GetTypes(),
				new NamedElementStrategy(_svcManager));
			// Test old service inherited functionality
			TestRegElements<TestNamedElementInfo>();
			var svc = _extManager.GetService<INamedSvc>();
			// Test new service inherited functionality
			TestRegElementsInService(svc);
			// Test own functionality
			Assert.IsFalse(svc.ContainsElement(""));
			Assert.IsTrue(svc.ContainsElement(SampleElement.Name));
			Assert.AreEqual(svc.GetElement(SampleElement.Name).ElementType,
				typeof (SampleElement));
		}
	}
}
using System;

using NUnit.Framework;

using R.Server.Common.BusinessLogic;
using R.Server.Common.ObjectModel;
using R.Server.Common.Services;

namespace Tests
{
	public class TestBase
	{
		[TestFixtureSetUp]
		public void FixtureSetUp()
		{
			SetUp();
		}

		protected virtual void SetUp()
		{
			DataAccessService.CreateAccountAccessor().
				ChangePassword(1, "test", null, PasswordFormat.Clear);
		}

		[TestFixtureTearDown]
		public void FixtureTearDown()
		{
			TearDown();
		}

		protected virtual void TearDown()
		{
		}

		public AccountManager AccountManager { get { return new AccountManager(); } }

		public TestDataAccessor DataAccessor { get { return TestDataAccessor.CreateInstance(); } }
	}
}
using System;
using System.Web.Security;

using NUnit.Framework;

using R.Server.Common.ObjectModel;

using Tests;

namespace Web.Common.Providers
{
	[TestFixture]
	public class RMembershipProviderTest : TestBase
	{
		[Test]
		public void GetUser()
		{
			MembershipUser user = Membership.GetUser("Admin");

			Assert.IsNotNull(user);
		}

		[Test]
		public void GetUserOnline()
		{
			MembershipUser user = Membership.GetUser("Admin", true);
			AccountImpl        acc  = AccountManager.GetAccountByName(user.UserName);

			Assert.IsTrue((DateTime.UtcNow - acc.LastActivityDate).TotalSeconds <= 1);
		}

		[Test]
		public void ChangePassword()
		{
			Membership.Provider.ChangePassword("admin", "test", "test");

			AccountImpl acc = AccountManager.GetAccountByID(1);


		}
	}
}

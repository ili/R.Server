using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

using NUnit.Framework;

using R.Server.ClientCommon;
using R.Server.ClientModel;
using R.Server.Common;

namespace Tests.Communications
{
	[TestFixture]
	public class AuthTest : CommTestBase
	{
		[SetUp]
		public void SetUp()
		{
		}

		[Test]
		[ExpectedException(typeof (SecurityAccessDeniedException))]
		public void BadUserName()
		{
			ProxyManager.Credentials = new RServerCredentials("Config", "QQQ", "bp");
			ProxyManager.CreateProxy<IClientProxy>("RServerInfo").GetInfo();
		}

		[Test]
		[ExpectedException(typeof (SecurityAccessDeniedException))]
		public void BadPassword()
		{
			ProxyManager.Credentials = new RServerCredentials("Config", "TestUser", "bp");
			ProxyManager.CreateProxy<IClientProxy>("RServerInfo").GetInfo();
		}

		[Test]
		[ExpectedException(typeof (FaultException))]
		public void BadAuthenticationType()
		{
			ProxyManager.Credentials = new RServerCredentials("BConfig", "TestUser", "pwd");
			ProxyManager.CreateProxy<IClientProxy>("RServerInfo").GetInfo();
		}

		[Test]
		/// <summary>
		/// This test require enabled native authentication and active common database.
		/// </summary>
		public void NativeAuthentication()
		{
			ProxyManager.Credentials = new RServerCredentials("Native", "TestUser", "pwd");
			ProxyManager.CreateProxy<IClientProxy>("RServerInfo").GetInfo();
		}

		[Test]
		[ExpectedException(typeof (SecurityAccessDeniedException))]
		public void ServerAccessDenied()
		{
			ProxyManager.Credentials = new RServerCredentials("Config", "TestUser", "pwd");
			using (var proxy =
				ProxyManager.CreateProxy<TaskManagerTest.IClientProxy>(TaskManagerCommService.ServiceName))
				proxy.Run("", null);
		}

		/// <summary>
		/// This test require windows user with name 'TestUser' and password 'tpwd',
		/// that member of 'RServerUsers' group.
		/// </summary>
		[Test]
		public void WindowsAuthentication()
		{
			ProxyManager.Credentials = new RServerCredentials("Windows", "TestUser", "tpwd");
			ProxyManager.CreateProxy<IClientProxy>("RServerInfo").GetInfo();
		}

		/// <summary>
		/// This test require enabled anonymous authentication.
		/// </summary>
		[Test(Description = "Anonymous authentication test.")]
		public void AnonymousAuthentication()
		{
			ProxyManager.Credentials = new RServerCredentials("Anonymous", "", "");
			ProxyManager.CreateProxy<IClientProxy>("RServerInfo").GetInfo();
		}

		#region IClientProxy interface
		public interface IClientProxy : IRServerInfoService, IChannel, IDisposable
		{
		}
		#endregion
	}
}
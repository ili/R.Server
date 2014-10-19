using System;
using System.ServiceModel.Channels;

using NUnit.Framework;

using R.Server.ClientModel;

namespace Tests.Communications
{
	[TestFixture]
	public class RServerInfoServiceTest : CommTestBase
	{
		[Test]
		public void TestGetInfo()
		{
			using (var proxy = ProxyManager.CreateProxy<IClientService>("RServerInfo"))
			{
				var info = proxy.GetInfo();
				proxy.GetInfo();
				Assert.IsFalse(string.IsNullOrEmpty(info.Name));
				Assert.IsFalse(string.IsNullOrEmpty(info.Description));
			}
		}

		public interface IClientService : IRServerInfoService, IChannel, IDisposable
		{
		}
	}
}
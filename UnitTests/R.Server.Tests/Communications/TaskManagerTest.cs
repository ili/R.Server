using System;
using System.ServiceModel.Channels;

using NUnit.Framework;

using R.Server.ClientModel;
using R.Server.Common;

namespace Tests.Communications
{
	[TestFixture]
	public class TaskManagerTest : CommTestBase
	{
		[Test]
		public void EchoTest()
		{
			using (var proxy = ProxyManager.CreateProxy<IClientProxy>(TaskManagerCommService.ServiceName))
			{
				const string testString = "Test String";
				var res = proxy.Run(EchoTask.TaskName,
					new[] {new TaskParameter(EchoTask.MessageParamName, testString)});
				Assert.AreEqual(testString, res.Result);
			}
		}

		#region IClientProxy interface
		internal interface IClientProxy : ITaskManagerCommService, IChannel, IDisposable
		{
		}
		#endregion
	}
}
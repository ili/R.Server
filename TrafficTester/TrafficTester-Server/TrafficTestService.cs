using System;
using System.ServiceModel;

using R.Server.Common;

namespace Rsdn.TrafficTester
{
	[RCommService(
		TrafficTestHelper.ServiceName,
		typeof (ITrafficTestService),
		InstanceContextMode.PerCall)]
	internal class TrafficTestService : ITrafficTestService
	{
		private const int _sizeLimit = 16777216; //16M

		#region Implementation of ITrafficTestService
		public bool UploadPackage(TestPackage package)
		{
			return package.CheckHash();
		}

		public TestPackage DownloadPackage(int size)
		{
			if (size <=0 || size > _sizeLimit)
				throw new ArgumentOutOfRangeException("size");
			return TrafficTestHelper.CreateTestPackage(size);
		}
		#endregion
	}
}
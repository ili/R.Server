using System.ServiceModel;

namespace Rsdn.TrafficTester
{
	[ServiceContract]
	public interface ITrafficTestService
	{
		[OperationContract]
		bool UploadPackage(TestPackage package);

		[OperationContract]
		TestPackage DownloadPackage(int size);
	}
}
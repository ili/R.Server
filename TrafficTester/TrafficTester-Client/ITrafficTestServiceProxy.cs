using System.ServiceModel;

using Rsdn.TrafficTester;

namespace TrafficTester_Client
{
	public interface ITrafficTestServiceProxy : ITrafficTestService, IClientChannel
	{}
}
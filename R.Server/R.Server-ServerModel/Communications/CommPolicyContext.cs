using System;

namespace R.Server.ServerModel
{
	public class CommPolicyContext
	{
		public CommPolicyContext(string serviceName, Type serviceType, string methodName)
		{
			ServiceName = serviceName;
			MethodName = methodName;
			ServiceType = serviceType;
		}

		#region ICommPolicyContext Members
		public string ServiceName { get; private set; }

		public Type ServiceType { get; private set; }

		public string MethodName { get; private set; }
		#endregion
	}
}
using System;
using System.Runtime.Serialization;

namespace Rsdn.TrafficTester
{
	[DataContract]
	public class TestPackage
	{
		public TestPackage(byte[] data, byte[] hash)
		{
			if (data == null)
				throw new ArgumentNullException("data");
			if (hash == null)
				throw new ArgumentNullException("hash");
			Data = data;
			Hash = hash;
		}

		public TestPackage()
		{}

		[DataMember]
		public byte[] Data { get; set; }

		[DataMember]
		public byte[] Hash { get; set; }
	}
}
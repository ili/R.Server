using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Linq;

namespace Rsdn.TrafficTester
{
	/// <summary>
	/// Helper methods.
	/// </summary>
	public static class TrafficTestHelper
	{
		public const string ServiceName = "TrafficTestService";

		public static byte[] CalculateHash(this byte[] data)
		{
			if (data == null)
				throw new ArgumentNullException("data");
			return new SHA1Managed().ComputeHash(data);
		}

		public static TestPackage CreateTestPackage(this byte[] data)
		{
			return new TestPackage(data, data.CalculateHash());
		}

		public static TestPackage CreateTestPackage(int size)
		{
			var data = new byte[size];
			var rnd = new Random((int)(Stopwatch.GetTimestamp() & 0x000000FFFFFF));
			rnd.NextBytes(data);
			return CreateTestPackage(data);
		}

		public static bool CheckHash(this byte[] data, byte[] hash)
		{
			return 
				new SHA1Managed()
					.ComputeHash(data)
					.SequenceEqual(hash);
		}

		public static bool CheckHash(this TestPackage package)
		{
			return package.Data.CheckHash(package.Hash);
		}
	}
}
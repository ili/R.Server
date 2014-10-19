using System;

using BLToolkit.DataAccess;

namespace Tests
{
	public abstract class TestDataAccessor : DataAccessor
	{
		public static TestDataAccessor CreateInstance()
		{
			return CreateInstance<TestDataAccessor>();
		}
	}
}

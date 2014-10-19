using NUnit.Framework;

namespace Tests
{
	public class RServerTestBase
	{
		[TestFixtureSetUp]
		public virtual void TextFixtureSetUp()
		{
		}

		[TestFixtureTearDown]
		public virtual void TestFixtureTearDown()
		{
		}
	}
}
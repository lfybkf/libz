using System;
using BDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
	[TestClass]
	public class testBently
	{
		[TestMethod]
		public void TestConnection()
		{
			EC.defaultStore = new SqlCE("Onto");
			bool ok = EC.defaultStore.TestConnection();

			EC ec = new EC();
			Assert.IsTrue(ok);

		}
	}
}

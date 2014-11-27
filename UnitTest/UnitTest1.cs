using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDB;

namespace UnitTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestPropis()
		{
			Func<long, bool, string> Do = Propis.Go;
			string s;
			Assert.AreEqual("сто двадцать три", Do(123, false));
			Assert.AreEqual("сто двадцать три", Do(123, true));

			s = Do(501890, true);
			Assert.AreEqual("пятьсот одна тысяча восемьсот девяносто ", s);
		}//function
	}//class
}//ns

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDB;

namespace UnitTest
{
	[TestClass]
	public class TestIni
	{
		[TestMethod]
		public void tiniGet()
		{
			var path = "test.ini";
			var ini = Ini.Load(path, true);
			ini.set("intValue", 7);
			ini.set("strValue", "Cumba");
			ini.set("arrValue", new string[] {"1a", "2b", "3c", "4d" });
			ini.Save();

			ini.Clear();

			ini = Ini.Load(path, false);
			var i = ini.getInt("intValue");
			var s = ini.getString("strValue");
			var arr = ini.getArray("arrValue");
			Assert.IsTrue(arr.Any());
		}//function
	}//class
}//namespace

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
			var name = "test";
			var ini = Ini.Load(name, true);
			ini.set("intValue", 5);
			ini.set("strValue", "Pumba");
			ini.set("arrValue", new string[] {"1a", "2b", "3c" });
			ini.Save();

			ini.Clear();

			ini = Ini.Load(name, true);
			var i = ini.getInt("intValue");
			var s = ini.getString("strValue");
			var arr = ini.getArray("arrValue");
			Assert.IsTrue(arr.Any());
		}//function
	}//class
}//namespace

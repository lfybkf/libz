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


		class Value {
			public int intValue { get; set; }
			public string strValue { get; set; }
			public string[] arrValue { get; set; }
		}
		class Bu
		{
			public int intBu { get; set; }
			public string strBu { get; set; }
			public string[] arrBu { get; set; }
		}

		[TestMethod]
		public void tiniSerialize()
		{
			var path = "test.ini";
			var ini = Ini.Load(path, true);

			Value val = ini.DeSerialize<Value>();
			Bu bu = ini.DeSerialize<Bu>();
			Assert.IsTrue(val.intValue == 7);

			bu.arrBu = new string[] {"bu3", "bu166" };
			bu.intBu += 11;
			bu.strBu += "B";
			ini.Serialize(bu);
			ini.Save();
		}//function
	}//class
}//namespace

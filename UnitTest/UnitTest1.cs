using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDB;
using BDB.Templating;

namespace UnitTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestPropis()
		{
			string s = "сто двадцать";
			Assert.AreEqual(s.addDelim(" ", "одна"), Propis.Go(121, false));
			Assert.AreEqual("сто двадцать один", Propis.Go(121, true));
			Assert.AreEqual("сто двадцать один", Propis.Go(121));

			s = Propis.Go(501890);
			Assert.AreEqual("пятьсот одна тысяча восемьсот девяносто ", s);
		}//function

		[TestMethod]
		public void TestSql()
		{
			Map map = new Map();
			map.Load();
			Action<string> write = AC.Instance.log.Info;
			foreach (var obj in map.objects) 
			{ 
				write(obj.ReadID());
				foreach (var field in obj.fields)
				{
					write(field.ReadField());
				}//for
				write("==============");
			}//for
			
			//foreach (var obj in map.objects) { write(obj.sqlLoad); }//for
			//foreach (var obj in map.objects)	{	write(obj.sqlInsert);	}//for
			//foreach (var obj in map.objects) { write(obj.sqlUpdate); }//for
			//foreach (var obj in map.objects) { write(obj.sqlDelete); }//for
		}//function
	}//class
}//ns

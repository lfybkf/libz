using System;
using System.Linq;
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

		[TestMethod]
		public void TestEnumerator()
		{
			Action<string> write = AC.Instance.log.Info;

			//string[] array = { "kaba", "muba", "taba", "zaba" };
			int[] array = Enumerable.Range(-2, 8).ToArray();
			foreach (var item in array.sequenceSwing(10))
			{
				write(item.ToString());	
			}//for

			write("======================");

			foreach (var item in array.sequenceCircle(10))
			{
				write(item.ToString());
			}//for
		}//function
	}//class
}//ns

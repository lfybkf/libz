using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDB;

namespace UnitTest
{
	class Goo 
	{
		public int Age { get; set; }
		public string Name { get; set; }
		public DateTime Now() { return DateTime.Now; }
		public override string ToString()
		{
			return Name.ToUpper() + "!!";
		} 
	}
	class Goo2 : Goo { public string Tim(string s1, string s2) { return s1 + Age + s2; } }

	[TestClass]
	public class TestExtension
	{
		[TestMethod]
		public void TestFmto()
		{
			string s = "My name is {name} and my age is {age}, so call me {name}!{bo}";
			//var phS = s.getPlaceholders();
			//Assert.IsTrue(phS.Values.Contains("{name}"));
			//Assert.IsTrue(phS.Values.Contains("{age}"));

			var o = new { name = "Барсук", age = 38};
			string val = o.getPropertyValue("name");
			Assert.AreEqual("Барсук", val);

			string sfmto = s.fmto(o);
			Assert.AreEqual("My name is Барсук and my age is 38, so call me Барсук!", sfmto);

			var goo = new Goo2 { Age = 44, Name = "Пятнадцатов" };
			s = "name={{Name}{Age}}. {Tim(A,F)} Fre {ToString()}. Tim={Tim(БАКЛЯ,букля)}";
			sfmto = s.fmto(goo);
			Assert.IsTrue(sfmto != string.Empty);
		}//function

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
		public void TestEnumerator()
		{
			Action<string> write = SimpleLogger.Instance.Info;
			int n = 0;
			string[] array = { "kaba", " muba", "  taba", "   ourt", "    ifqa", "      zaba" };
			//int[] array = Enumerable.Range(-2, 6).ToArray();
			array.sequenceSwing(n).forEach(o => write(o.ToString()));
			write("======================");
			array.sequenceCircle(n).forEach(o => write(o.ToString()));
			write("======================");
			array.sequenceRandom(n*3).forEach(o => write(o.ToString()));
		}//function

		[TestMethod]
		public void TestString()
		{
			Action<string> write = SimpleLogger.Instance.Info;
			string s;
			Assert.IsTrue((s = "Omnibus".after("ib")) == "us");
			Assert.IsTrue((s = "Omnibus".after("Omni")) == "bus");
			Assert.IsTrue("Omnibus".before("ib") == "Omn");
			Assert.IsTrue("Omnibus".after("zz") == "");
			Assert.IsTrue("Omnibus".midst("Om", "us") == "nib");
			Assert.IsTrue("Omnibus".midst("zz", "us") == "Omnib");
			Assert.IsTrue("Omnibus".midst("Om", "zz") == "nibus");
			Assert.IsTrue("func(FIND ME, !isHere)".midst("(", ", ") == "FIND ME");

			Assert.IsTrue("26.01.2015".parse(new DateTime(2001, 12,31)).Equals(new DateTime(2015,01,26)));
			Assert.IsTrue("26,01,2015".parse(new DateTime(2001, 12, 31)).Equals(new DateTime(2015, 01, 26)));
			Assert.IsTrue("26/01/2015".parse(new DateTime(2001, 12, 31)).Equals(new DateTime(2015, 01, 26)));
			Assert.IsTrue("ttt26.01.2015".parse(new DateTime(2001, 12, 31)).Equals(new DateTime(2001, 12, 31)),  "wrong date");
			Assert.IsTrue("1.00".parse(Decimal.MinusOne) == 1M, "wrong decimal");
			Assert.IsTrue("4 145.40".parse(Decimal.MinusOne) == 4145.4M, "wrong decimal");
			Assert.IsTrue("4".parse(0) == 4, "wrong int");


		}//function
	}//class
}//ns

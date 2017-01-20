using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDB;
using System.Collections.Generic;

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

			var dict = new Dictionary<string, string>() { { "name", "AA" }, { "age", "55" } };
			s = "name={name} age={age} Name={Name} {Tim(A,F)} {NONONO} {Name}"; 
			sfmto=s.fmto(dict, goo);
			Assert.IsTrue(sfmto != string.Empty);
		}//function

		[TestMethod]
		public void TestTranslate()
		{
			string s = "СП2";
			Assert.AreEqual(s.translate("СП2", "SP4"), "SP4");
			Assert.AreEqual(s.translate("СП2", "SP"), "SP2");
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

			Assert.IsTrue((s = "OMNIBUS".beforeCI("ib")) == "OMN");
			Assert.IsTrue((s = "OMNIBUS".afterCI("iB")) == "US");
			Assert.IsTrue((s = "OMNIBUS".midstCI("Om", "us")) == "NIB");

			Assert.IsTrue("26.01.2015".parse(new DateTime(2001, 12,31)).Equals(new DateTime(2015,01,26)));
			Assert.IsTrue("26,01,2015".parse(new DateTime(2001, 12, 31)).Equals(new DateTime(2015, 01, 26)));
			Assert.IsTrue("26/01/2015".parse(new DateTime(2001, 12, 31)).Equals(new DateTime(2015, 01, 26)));
			Assert.IsTrue("ttt26.01.2015".parse(new DateTime(2001, 12, 31)).Equals(new DateTime(2001, 12, 31)),  "wrong date");

			Dictionary<string, decimal> testSD = new Dictionary<string, decimal>();
			testSD.Add("1.00", 1M);
			testSD.Add("4 145,00", 4145.0M);
			testSD.Add("4", 4M);
			testSD.Add("0.5", 0.5M);
			testSD.Add("6123.45", 6123.45M);
			testSD.Add("7 123,45", 7123.45M);
			foreach (var item in testSD)
			{
				Assert.IsTrue(item.Key.parse(Decimal.MinusOne) == item.Value, "wrong decimal " + item.Key);	
			}//for

			//testSD = new Dictionary<string, decimal>();
			testSD.Add("cdj5vj9  01vd,f 0..,0vp1==", 5901.001M);
			testSD.Add("cdj5vj9  01vd,f 90vp", 5901.90M);
			testSD.Add(".00", 0M);
			testSD.Add("cdsu7vw80-v90", 78090M);
			testSD.Add("cdj5vj9  01vd,f 9...,0vp", 5901.90M);
			testSD.Add("156.0008", 156.0008M);
			testSD.Add("156,00 08", 156.0008M);
			foreach (var item in testSD)
			{
				Assert.IsTrue(item.Key.parseDecimal() == item.Value, "wrong decimal " + item.Key);
			}//for

		}//function

		[TestMethod]
		public void TestAdd()
		{
			string s = null;
			Assert.IsTrue(s.addSpace("mu", "MU") == "mu MU");

			Assert.IsTrue("1".addSpace("mu", "MU") == "1 mu MU");
		}//function

		[TestMethod]
		public void textTakeLast()
		{
			string s = Environment.CurrentDirectory;
			string sP;
			var chars = s.AsEnumerable().Take(s.Length - 1);
			sP = chars.toString();
			sP = chars.TakeUntil(ch => ch == '\\').toString();
			sP = chars.TakeUntilLast(ch => ch == '\\').toString();

			Assert.IsTrue("1".addSpace("mu", "MU") == "1 mu MU");
		}//function

		[TestMethod]
		public void textShaffle()
		{
			int R = 14;
			var ii = Enumerable.Range(1, R).ToArray();
			string sR, sS;
			string[] ss = new string[R-1];
			for (int i = 0; i < ss.Length; i++)
			{
				ss[i] = ii.sequenceSkip(i+1).print(null, " ");
			}
			sR = ii.orderByRandom().print(null, " ");
			ii.shuffle();
			sS = ii.print(null, " ");
			Assert.IsTrue(ss.All(z => z.Length == sR.Length));
		}//function
	}//class
}//ns

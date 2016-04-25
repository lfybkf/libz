using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDB.Owin.Razor;

namespace UnitTest
{
	[TestClass]
	public class TestWeb
	{
		[TestMethod]
		public void TestHelpers()
		{
			string link = "СП".tagLink("/Dog"); //.addSlash("Index", "CG")
			string tr = "td".tagThem("Код", "Номер");
			return;
		}
	}//class
}//namespace

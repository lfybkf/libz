using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDB;

namespace UnitTest
{
	[TestClass]
	public class TestDirectory
	{
		[TestMethod]
		public void testDir()
		{
			string path = @"C:\temp";
			var di = new System.IO.DirectoryInfo(path);
			string dir1 = di.add("first", "second");
			string sub = di.subPath(dir1);
			return;
		}
	}//class
}//namespace

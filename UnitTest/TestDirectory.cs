﻿using System;
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
			var fld3 = Folder.New(Environment.CurrentDirectory, false);
			var n3 = fld3.Name;

			string path = @"C:\temp";
			var fld = Folder.New(path);
			var fld2 = Folder.New(fld.Add("first", "second"));
			var par = fld2.Parent;
			var root = fld.Root;
			var name = fld2.Name;
			string sub = fld.SubPath(fld2);
			sub = fld - fld2;
			sub = fld2 - fld.Path;
			var files = fld.Files.Where(z => z.endsWithCI(".mine"));
			var dirs = fld.Directories;
			fld.ReRoot(@"D:\fu\fu");
			fld.Create();
			return;
		}
	}//class
}//namespace

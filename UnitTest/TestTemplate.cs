using BDB.Templating;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
	[TestClass]
	public class TestTemplate
	{
		[TestMethod]
		public void TestMachineConfig()
		{
			Action<string> write = BDB.SimpleLogger.Instance.Info;
			Map map = new Map();
			map.LoadMachines();
			Machine machine = map.machines.First();
			machine.Validate();

			Assert.AreEqual(machine["TestAttr"], "Колбаса");

			write("==errors=======");
			machine.errors.forEach(z => write(z));
			write("==warning=======");
			machine.warnings.forEach(z => write(z));

		}//function

		[TestMethod]
		public void TestStringTemplate()
		{
			string tmpl = "if ({Name} > 3) { return null; } //{Age}";
			string res = tmpl.fmto(new { Name = "Var", Age = 511 });
			Assert.AreEqual(res, "if (Var > 3) { return null; } //511");

		}//function
	}//class
}//ns

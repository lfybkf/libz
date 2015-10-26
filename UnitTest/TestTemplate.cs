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
			var errors = machine.Validate();
			errors.forEach(z => write(z));
		}//function

	}//class
}//ns

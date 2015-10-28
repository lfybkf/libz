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
	public class TestMachine
	{
		[TestMethod]
		public void TestMachineOne()
		{
			smDialog machine = new smDialog();
			machine.Push(smDialogPUSH.Start);
			Assert.IsTrue(machine.IsPushOK);
			machine.Push(smDialogPUSH.Start);
			Assert.IsTrue(machine.IsPushOK == false);

		}//function

	}//class
}//ns

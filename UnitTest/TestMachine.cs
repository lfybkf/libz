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
			Action<object> write = BDB.SimpleLogger.Instance.Info;
			smDialog machine = new smDialog();
			machine.dev1 = new DevOne { IsOnline = true, IsOK = true };
			machine.dev2 = new DevTwo { IsOK = true };
			machine.Push(smDialogPUSH.Prepare);
			write(machine.BadCheck);
			Assert.IsTrue(machine.IsPushOK);
			machine.Push(smDialogPUSH.Select);
			write(machine.BadCheck);
			Assert.IsTrue(machine.IsPushOK);


		}//function

	}//class
}//ns

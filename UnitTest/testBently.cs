using System;
using BDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace UnitTest
{
	[TestClass]
	public class testBently
	{
		[TestMethod]
		public void TestConnection()
		{
			EC.defaultStore = new SqlCE("Onto");
			bool ok = EC.defaultStore.TestConnection();

			EC ec = new EC();
			Assert.IsTrue(ok);
		}

		[TestMethod]
		public void TestMaxMin()
		{
			EC.defaultStore = new SqlCE("Onto");
			var ec = new EC();
			var min = ec.Min("ID", "Author");
			var max = ec.Max("ID", "Author");

			long minL = ec.Max("ID", "Author", 0);
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestInsert()
		{
			var store = new SqlCE("Onto");
			DateTime dt = DateTime.Now;
			var cmd = store.getCommand("insert into Author(Name, DtCreated) values (@Name, @Dt)");
			var par1 = store.getParameter();
			var par2 = store.getParameter();
			par1.ParameterName = "Name"; par1.DbType = System.Data.DbType.String; par1.Value = "Bu" + dt.Second.ToString();
			par2.ParameterName = "Dt"; par2.DbType = System.Data.DbType.DateTime; par2.Value = dt;
			cmd.Parameters.Add(par1); cmd.Parameters.Add(par2);
			store.Execute(cmd);

			//cmd = store.getCommand("SELECT @@Identity");
			//cmd = store.getCommand("SELECT scope_identity()");
			cmd = store.getCommand("SELECT Max(ID) from Author");
			var newId = store.Scalar(cmd);
			var err = store.LastError;

			
			Assert.IsTrue(newId != null);
		}

		[TestMethod]
		public void TestScalar()
		{
			

			var store = new SqlCE("Onto");
			var cmd = store.getCommand (new SqlBuilder { Table = "Author" }.Count);
			var count = store.Scalar(cmd);
			var err = store.LastError;

			cmd = store.getCommand(new SqlBuilder { Table = "Author", Field = "ID", Where="ID=-1", Top=1 }.Select);
			var has = store.Scalar(cmd);

			Assert.IsTrue(count != null);
		}


	}
}

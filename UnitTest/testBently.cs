﻿using System;
using BDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace UnitTest
{
	[TestClass]
	public class testBently
	{
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

		[TestMethod]
		public void Test_SqlF()
		{
			string s1 = SqlF.AND(new string[] {SqlF.In("ID", new long[] { 1655, 1653 }), SqlF.Eq("Name", "BUKA") });
			string s2 = SqlF.OR(new string[] { SqlF.In("Name", new string[] { "Abc", "Que", "BRI" }, false), SqlF.Eq("ID", 300) });
			string s3 = SqlF.AND(new string[] { SqlF.In("Item", new int[] { 34528, 31259 }), SqlF.Like("Name", "BUKA") });

			var sqlb = new SqlBuilder { Table = "Img", Top = 15,
				Where = SqlF.AND(
					//SqlF.InList("ID", new long[] { 1655, 1653 }),
					SqlF.OR(SqlF.Eq("ID", 1655), SqlF.Eq("ID", 1653)),
					SqlF.Like("Name", "%31%"),
					SqlF.Between("ID", 1000, 3000)
				)
			};
			string sql = sqlb.Select;
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void Test_sqlBuilder()
		{
			string sql;
			var sqlb = new SqlBuilder
			{
				Table = "ValRef",
				Having = SqlF.Eq(SqlF.COUNT, 3)
			};
			sql = sqlb.fGroup("DimID", "Val", SqlF.COUNT, "-1 as ID, 0 as Item, -1 as ImgID").fOrder("DimID").Select;
			Assert.IsTrue(sql != null);

			sqlb = new SqlBuilder { Table = "Ref", Distinct=true, Top=4, Where=SqlF.Gt("ID", 0)};
			sql = sqlb.Select;
			Assert.IsTrue(sql != null);
		}//function
	}
}

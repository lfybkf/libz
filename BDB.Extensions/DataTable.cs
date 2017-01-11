using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BDB
{
	///<summary>DataTableExtensions</summary> 
	public static class DataTableExtensions
	{
		///<summary>set pk</summary> 
		public static void SetPK(this DataTable dt, string Name)
		{
			DataColumn dc = dt.Columns[Name];
			if (dc == null)
				return;

			dt.PrimaryKey = new DataColumn[] { dc};
		}//func

		///<summary>set pk</summary> 
		public static void SetPK(this DataTable dt, string Name1, string Name2)
		{
			DataColumn dc1 = dt.Columns[Name1];
			DataColumn dc2 = dt.Columns[Name2];
			if (dc1 == null || dc2 == null)
				return;

			dt.PrimaryKey = new DataColumn[] { dc1, dc2 };
		}//func

		///<summary>SetAutoincrement</summary>
		public static void SetAutoincrement(this DataTable dt, string Name, int Seed, int Step)
		{
			DataColumn dc = dt.Columns[Name];
			dc.AutoIncrement = true;
			dc.AutoIncrementStep = Step;
			dc.AutoIncrementSeed = Seed;
		}//func
	}//class
}//ns

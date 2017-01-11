using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BDB
{
	///<summary>DataGridViewExtensions</summary> 
	public static class DataGridViewExtensions
	{
		///<summary>getCurrent</summary> 
		public static object getCurrent(this DataGridView grid, string Column)
		{
			if (grid.CurrentRow == null)
				return null;
			
			return grid.CurrentRow.Cells[Column].Value;
		}//func

		///<summary>setCurrent on value</summary> 
		public static void setCurrent(this DataGridView grid, string Column, object value)
		{
			if (grid.CurrentRow == null)
				return;

			grid.CurrentRow.Cells[Column].Value = value;
		}//func
	}//class
}//ns

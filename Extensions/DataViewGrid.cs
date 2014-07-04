using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BDB
{
	public static class DataGridViewExtensions
	{
		public static object GetCurrent(this DataGridView grid, string Column)
		{
			if (grid.CurrentRow == null)
				return null;
			
			return grid.CurrentRow.Cells[Column].Value;
		}//func

		public static void SetCurrent(this DataGridView grid, string Column, object value)
		{
			if (grid.CurrentRow == null)
				return;

			grid.CurrentRow.Cells[Column].Value = value;
		}//func
	}//class
}//ns

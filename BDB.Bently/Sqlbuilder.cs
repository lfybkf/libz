using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	///<summary>Построитель SQL</summary>
	public class SqlBuilder
	{
		const string allFields = S.Star;

		///<summary>таблица</summary>
		public string Table;
		///<summary>SELECT {Field}, UPDATE {Field} = {Value}</summary>
		public string Field;
		///<summary>sql WHERE {Where}</summary>
		public string Where;
		///<summary>UPDATE TABLE {Set} WHERE</summary>
		public string Set;
		///<summary>UPDATE {Field} = {Value}; INSERT ... VALUES ({Value})</summary>
		public object Value;
		///<summary>SELECT {Top}</summary>
		public int Top = 0;
		///<summary>SELECT {DISTINCT}</summary>
		public bool Distinct = false;
		///<summary>SELECT ... ORDER BY {Order}</summary> 
		public string Order;
		///<summary>SELECT ... GROUP BY {Group}</summary> 
		public string Group;
		///<summary>SELECT ... HAVING {Having}</summary> 
		public string Having;

		///<summary>стереть все кроме таблицы (она часто не меняется)</summary>
		public void ClearButTable()
		{
			string tmp = this.Table;
			ClearAll();
			this.Table = tmp;
		}//function

		///<summary>стереть все</summary>
		public void ClearAll()
		{
			Table = null;
			Field = Where = Set = Order = Group = Having = null;
			Value = null;
			Top = 0;
		}//function

		///<summary>тот же Set, но для массива строк</summary> 
		public SqlBuilder fSet(params string[] sets)
		{
			if (!sets.Any()) { return this; }
			Set = sets.print(null, S.Comma);
			return this;
		}//function


		///<summary>тот же Order, но для массива строк</summary> 
		public SqlBuilder fOrder(params string[] fields)
		{
			if (!fields.Any()) { return this; }
			Order = fields.print(null, S.Comma);
			return this;
		}//function

		static readonly string[] aggrs = {"MAX", "MIN", "COUNT", "AVG"};
		///<summary>одновременное заполнение Group и Field, поля MAX, MIN, COUNT, AVG, [ as ] - включаются только в Field</summary> 
		public SqlBuilder fGroup(params string[] fields)
		{
			if (!fields.Any()) { return this; }
			Func<string, bool> groupField = (s) => { return !s.containsCI(" as ") && !aggrs.Any(a => s.StartsWith(a)); };
			Group = fields.Where(z => groupField(z)).print(null, S.Comma);
			Field = fields.print(null, S.Comma);
			return this;
		}//function

		///<summary>SELECT TOP {Top} {Field} FROM {Table} WHERE {Where} ORDER BY {Order}</summary>
		public string Select
		{
			get
			{
				StringBuilder sb = new StringBuilder("SELECT ");
				if (Distinct) { sb.Append("DISTINCT "); }
				if (Top > 0) { sb.AppendFormat("TOP {0} ", Top); }
				sb.Append(Field.isEmpty() ? allFields : Field);
				sb.AppendFormat(" FROM {0} ", Table);
				if (Where.notEmpty()) { sb.AppendFormat("WHERE {0}", Where); }
				if (Group.notEmpty())
				{
					sb.AppendFormat("GROUP BY {0} ", Group);
					if (Having.notEmpty()) { sb.AppendFormat("HAVING {0} ", Having); }
				}//if
				if (Order.notEmpty()) { sb.AppendFormat("ORDER BY {0} ", Order); }
				return sb.ToString();
			}
		}//function

		///<summary>INSERT INTO {Table} ({Field}) VALUES ({Value})</summary>
		public string Insert
		{
			get
			{
				return "INSERT INTO {0} ({1}) VALUES ({2})".fmt(Table, Field, Value);
			}
		}//function

		///<summary>UPDATE {Table} SET {Set} WHERE {Where}, UPDATE {Table} SET {Field}={Value} WHERE {Where}</summary>
		public string Update
		{
			get
			{
				if (Set.notEmpty())
				{
					return "UPDATE {0} SET {1} WHERE {2}".fmt(Table, Set, Where);
				}//if

				if (Value is string)
				{
					return "UPDATE {0} SET {1}='{3}' WHERE {2}".fmt(Table, Field, Where, Value);
				}//if
				else
				{
					return "UPDATE {0} SET {1}={3} WHERE {2}".fmt(Table, Field, Where, Value);
				}//else
			}
		}//function

		///<summary>Table, Where</summary>
		public string Delete
		{
			get
			{
				return "DELETE FROM {0} WHERE {1}".fmt(Table, Where);
			}
		}//function

		///<summary>Field, Table, Where()</summary>
		public string Max
		{
			get
			{
				if (Where.isEmpty())
				{
					return "SELECT Max({0}) FROM {1}".fmt(Field, Table);
				}//if
				else
				{
					return "SELECT Max({0}) FROM {1} WHERE {2}".fmt(Field, Table, Where);
				}//else
			}
		}//function

		///<summary>Field, Table, Where()</summary>
		public string Min
		{
			get
			{
				if (Where.isEmpty())
				{
					return "SELECT Min({0}) FROM {1}".fmt(Field, Table);
				}//if
				else
				{
					return "SELECT Min({0}) FROM {1} WHERE {2}".fmt(Field, Table, Where);
				}//else
			}
		}//function

		///<summary>Table, Field(*), Where()</summary>
		public string Count
		{
			get
			{
				string _field = Field.isEmpty() ? allFields : Field;
				if (Where.isEmpty())
				{
					return "SELECT Count({0}) FROM {1}".fmt(_field, Table);
				}//if
				else
				{
					return "SELECT Count({0}) FROM {1} WHERE {2}".fmt(_field, Table, Where);
				}//else
			}
		}//function
	}//class
}//namespace

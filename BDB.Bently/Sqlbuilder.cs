using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	public class SqlBuilder
	{
		const string allFields = "*";

		public string Table;
		public string Field;
		public string Where;
		public object Value;
		public long LongValue;

		public string Select
		{
			get
			{
				string _field = Field.isEmpty() ? allFields : Field;
				if (Where.isEmpty())
				{
					return "SELECT {0} FROM {1}".fmt(_field, Table);
				}//if
				else
				{
					return "SELECT {0} FROM {1} WHERE {2}".fmt(_field, Table, Where);
				}//else
			}
		}//function

		public string Update
		{
			get
			{
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

		public string Delete
		{
			get
			{
				return "DELETE FROM {0} WHERE {1}".fmt(Table, Where);
			}
		}//function

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
	}//class
}//namespace

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	///<summary>Построитель SQL</summary>
	public class SqlBuilder
	{
		const string allFields = "*";
		
		#region fmt
		const string _fmtFieldEqValue = "{0} = {1}";
		public string fmtFieldEqValue {get{return _fmtFieldEqValue;}}
		const string _fmtFieldGtValue = "{0} > {1}";
		public string fmtFieldGtValue {get{return _fmtFieldGtValue;}}
		const string _fmtFieldGeValue = "{0} >= {1}";
		public string fmtFieldGeValue {get{return _fmtFieldGeValue;}}
		const string _fmtFieldLsValue = "{0} < {1}";
		public string fmtFieldLsValue {get{return _fmtFieldLsValue;}}
		const string _fmtFieldLeValue = "{0} <= {1}";
		public string fmtFieldLeValue {get{return _fmtFieldLeValue;}}
		
		const string _fmtFieldEqString = "{0} = '{1}'";
		public string fmtFieldEqString {get{return _fmtFieldEqString;}}
		const string _fmtFieldEqPar = "{0} = @{1}";
		public string fmtFieldEqPar {get{return _fmtFieldEqPar;}}
		#endregion

		public string Table;
		///<summary>SELECT {Field}, UPDATE {Field} = {Value}</summary>
		public string Field;
		///<summary>sql WHERE {Where}</summary>
		public string Where;
		///<summary>UPDATE TABLE {Set} WHERE</summary>
		public string Set;
		public object Value;
		//public long LongValue;
		public int Top = 0;

		///<summary>SELECT TOP {Top} {Field} FROM {Table} WHERE {Where}</summary>
		public string Select
		{
			get
			{
				string _select = Top > 0 ? "SELECT TOP " + Top : "SELECT";
				string _field = Field.isEmpty() ? allFields : Field;
				if (Where.isEmpty())
				{
					return "{0} {1} FROM {2}".fmt(_select, _field, Table);
				}//if
				else
				{
					return "{0} {1} FROM {2} WHERE {3}".fmt(_select, _field, Table, Where);
				}//else
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

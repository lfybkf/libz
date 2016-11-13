using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	///<summary>набор статических функций для SQL</summary>
	public static class SqlF
	{
		const string fmtFieldEqValue = "{0} = {1}";
		///<summary>{0} = {1}</summary>
		public static string FieldEqValue(string fld, object val) { return fmtFieldEqValue.fmt(fld, val); }

		const string fmtFieldGtValue = "{0} > {1}";
		///<summary>{0} gt {1}</summary>
		public static string FieldGtValue(string fld, object val) { return fmtFieldGtValue.fmt(fld, val); }

		const string fmtFieldGeValue = "{0} >= {1}";
		///<summary>{0} ge {1}</summary>
		public static string FieldGeValue(string fld, object val) { return fmtFieldGeValue.fmt(fld, val); }

		const string fmtFieldLsValue = "{0} < {1}";
		///<summary>{0} ls {1}</summary>
		public static string FieldLsValue(string fld, object val) { return fmtFieldLsValue.fmt(fld, val); }

		const string fmtFieldLeValue = "{0} <= {1}";
		///<summary>{0} le {1}</summary>
		public static string FieldLeValue(string fld, object val) { return fmtFieldLeValue.fmt(fld, val); }

		const string fmtFieldEqString = "{0} = '{1}'";
		///<summary>{0} = '{1}'</summary>
		public static string FieldEqString(string fld, object val) { return fmtFieldEqString.fmt(fld, val); }

		const string fmtFieldEqPar = "{0} = @{1}";
		///<summary>{0} = @{1}. Если имя параметра не указано, то используется имя поля</summary>
		public static string FieldEqPar(string fld, string par = null)
		{ return fmtFieldEqPar.fmt(fld, par ?? fld); }

		const string and = " and ";
		///<summary>соединить условия из списка по И</summary>
		public static string ANDed(IEnumerable<string> whereS)
		{
			return string.Join(and, whereS);
		}//function

		const string or = " or ";
		///<summary>соединить условия из списка по ИЛИ</summary>
		public static string ORed(IEnumerable<string> whereS)
		{
			return string.Join(or, whereS);
		}//function
	}//class
}//namespace

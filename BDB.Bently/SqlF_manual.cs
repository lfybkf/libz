using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	public static partial class SqlF
	{
		///<summary>соединить условия из списка по И</summary>
		public static string AND(IEnumerable<string> whereS) { return string.Join(") and (", whereS).quote(C.LParenthesis); }//function
		///<summary>соединить условия из списка по И</summary>
		public static string AND(params string[] whereS) { return string.Join(") and (", whereS).quote(C.LParenthesis); }//function

		///<summary>соединить условия из списка по ИЛИ</summary>
		public static string OR(IEnumerable<string> whereS) { return string.Join(") or (", whereS).quote(C.LParenthesis); }//function
		///<summary>соединить условия из списка по ИЛИ</summary>
		public static string OR(params string[] whereS) { return string.Join(") or (", whereS).quote(C.LParenthesis); }//function

		///<summary>fld IS NULL</summary>
		public static string IsNull(string fld) { return "{0} IS NULL".fmt(fld); }//function
																																							///<summary>fld IS NOT NULL</summary>
		public static string IsNotNull(string fld) { return "{0} IS NOT NULL".fmt(fld); }//function

		///<summary>fld In list</summary>
		public static string InList<T>(string fld, IEnumerable<T> list, bool IsIn = true)
		{
			string sList;
			if (typeof(T) == typeof(string))
			{
				sList = string.Join(S.Comma, list.Select(s => s.ToString().quote(C.Quote)));
			}//if
			else
			{
				sList = string.Join(S.Comma, list.Select(s => s.ToString()));
			}//else

			if (IsIn)
			{
				return "{0} IN ({1})".fmt(fld, sList);
			}//if
			else
			{
				return "{0} NOT IN ({1})".fmt(fld, sList);
			}//else
		}//function

		///<summary>fld In select</summary>
		public static string InSelect(string fld, string select, bool IsIn = true)
		{
			if (IsIn)
			{
				return "{0} IN ({1})".fmt(fld, select);
			}//if
			else
			{
				return "{0} NOT IN ({1})".fmt(fld, select);
			}//else
		}//function

		///<summary>fld between</summary>
		public static string Beetween<T>(string fld, T valFrom, T valTo) where T: struct
		{
			return "{0} BETWEEN {1} AND {2}".fmt(fld, valFrom, valTo);
		}//function
	}//class
}
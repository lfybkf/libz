using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	public static partial class SqlF
	{
		private const string NOT = "NOT";
		///<summary>COUNT(*)</summary> 
		public const string COUNT = "COUNT(*)";

		///<summary>соединить условия из списка по И</summary>
		public static string AND(IEnumerable<string> whereS) { return string.Join(") and (", whereS).quote(C.LParenthesis); }//function
		///<summary>соединить условия из списка по И</summary>
		public static string AND(params string[] whereS) { return string.Join(") and (", whereS).quote(C.LParenthesis); }//function

		///<summary>соединить условия из списка по ИЛИ</summary>
		public static string OR(IEnumerable<string> whereS) { return string.Join(") or (", whereS).quote(C.LParenthesis); }//function
		///<summary>соединить условия из списка по ИЛИ</summary>
		public static string OR(params string[] whereS) { return string.Join(") or (", whereS).quote(C.LParenthesis); }//function

		///<summary>fld IS NULL</summary>
		public static string IsNull(string fld, bool IsPositive = true) {
			return "{0} IS {1} NULL".fmt(fld
				, IsPositive ? string.Empty : NOT);
		}//function

		///<summary>fld In list</summary>
		public static string In<T>(string fld, IEnumerable<T> list, bool IsPositive = true)
		{
			string sList;
			if (typeof(T) == typeof(string)) {
				sList = string.Join(S.Comma, list.Select(s => s.ToString().quote(C.Quote)));
			}//if
			else {
				sList = string.Join(S.Comma, list.Select(s => s.ToString()));
			}//else
			return "{0} {2} IN ({1})".fmt(fld, sList
				, IsPositive ? string.Empty : NOT);
		}//function

		///<summary>fld In select</summary>
		public static string In(string fld, string select, bool IsPositive = true)
		{
			return "{0} {2} IN ({1})".fmt(fld, select
				, IsPositive ? string.Empty : NOT);
		}//function

		///<summary>fld LIKE pattern</summary>
		public static string Like(string fld, string pattern, bool IsPositive = true)
		{
			return "{0} {2} LIKE '{1}'".fmt(fld, pattern
				, IsPositive ? string.Empty : NOT);
		}//function

		///<summary>fld LIKE par</summary>
		public static string LikePar(string fld, string par, bool IsPositive = true)
		{
			return "{0} {2} LIKE @{1}".fmt(fld, par
				, IsPositive ? string.Empty : NOT);
		}//function
		 ///<summary>fld LIKE par</summary>
		public static string LikePar(string fld) { return LikePar(fld, fld, true); }

		///<summary>fld between</summary>
		public static string Between<T>(string fld, T valFrom, T valTo, bool IsPositive = true) where T: struct
		{
			return "{0} {3} BETWEEN {1} AND {2}".fmt(fld, valFrom, valTo
				, IsPositive ? string.Empty : NOT);
		}//function
	}//class
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	public static partial class SqlF
	{
		private static string not(bool IsPositive) => IsPositive ? string.Empty : "NOT";

		///<summary>COUNT(*)</summary> 
		public const string COUNT = "COUNT(*)";

		///<summary>соединить условия из списка по И</summary>
		public static string AND(IEnumerable<string> whereS) => string.Join(") and (", whereS).quote(C.LParenthesis);
		///<summary>соединить условия из списка по И</summary>
		public static string AND(params string[] whereS) => string.Join(") and (", whereS).quote(C.LParenthesis);

		///<summary>соединить условия из списка по ИЛИ</summary>
		public static string OR(IEnumerable<string> whereS) => string.Join(") or (", whereS).quote(C.LParenthesis);
		///<summary>соединить условия из списка по ИЛИ</summary>
		public static string OR(params string[] whereS) => string.Join(") or (", whereS).quote(C.LParenthesis);

		///<summary>fld IS NULL</summary>
		public static string IsNull(string fld, bool IsPositive = true) => $"{fld} IS {not(IsPositive)} NULL";

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
			return $"{fld} {not(IsPositive)} IN ({sList})";
		}//function

		///<summary>fld In select</summary>
		public static string In(string fld, string select, bool IsPositive = true) => $"{fld} {not(IsPositive)} IN ({select})";
		///<summary>fld LIKE pattern</summary>
		public static string Like(string fld, string pattern, bool IsPositive = true) => $"{fld} {not(IsPositive)} LIKE '{pattern}'";
		///<summary>fld LIKE par</summary>
		public static string LikePar(string fld, string par, bool IsPositive = true) => $"{fld} {not(IsPositive)} LIKE @{par}";
		///<summary>fld LIKE par</summary>
		public static string LikePar(string fld) => LikePar(fld, fld, true);

		///<summary>fld between</summary>
		public static string Between<T>(string fld, T valFrom, T valTo, bool IsPositive = true) where T: struct
		{
			return $"{fld} {not(IsPositive)} BETWEEN {valFrom} AND {valTo}";
		}//function
	}//class
}
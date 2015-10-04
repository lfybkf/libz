using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BDB
{
	public static class StringExtension
	{
		const char cPoint = '.';
		const char cComma = ',';
		const char cColon = ':';
		const char cSemicolon = ';';

		public static string fmt(this string s, params object[] oo) { return string.Format(s, oo); }//func
		public static bool inThe(this string s, params string[] ss)		{return ss.Contains(s);	}//func

		/// <summary>
		/// берет кусок строки после <paramref name="Prefix"/>
		/// Пример: "Omnibus".after("b") = "us"
		/// </summary>
		/// <param name="s"></param>
		/// <param name="Prefix">искомая строка</param>
		/// <returns></returns>
		public static string after(this string s, string Prefix) 
		{
			int i = s.IndexOf(Prefix);
			if (i >= 0)
				return s.Substring(i + Prefix.Length);
			else
				return string.Empty;
		}//func

		public static string before(this string s, string Suffix)
		{
			int i = s.IndexOf(Suffix);
			if (i > 0)
				return s.Substring(0, i);
			else
				return string.Empty;
		}//func

		public static string addToPath(this string s, params string[] args)
		{
			foreach (string path in args)
			{
				s = System.IO.Path.Combine(s, path);
			}//for
			return s;
		}//func

		public static string add(this string s, params object[] args)
		{
			StringBuilder sb = new StringBuilder(s);
			args.forEach(o => sb.Append(o));
			return sb.ToString();
		}//function

		public static string addLine(this string s, params object[] args)
		{
			StringBuilder sb = new StringBuilder(s);
			args.forEach(o => sb.AppendFormat("{0}{1}", Environment.NewLine, o));
			return sb.ToString();
		}//function

		public static string addDelim(this string s, string Delim, params object[] args)
		{
			StringBuilder sb = new StringBuilder(s);
			args.forEach(o => sb.AppendFormat("{0}{1}", Delim, o));
			return sb.ToString();
		}//function

		public static DateTime parse(this string s, DateTime def, IFormatProvider formatProvider = null)
		{
			DateTime z;
			if (formatProvider == null)
			{
				return DateTime.TryParse(s, out z) ? z : def;	
			}//if
			else
			{
				return DateTime.TryParse(s, formatProvider, DateTimeStyles.None, out z) ? z : def;	
			}//else
			
		}//function


		public static Decimal parse(this string s, Decimal def, IFormatProvider formatProvider = null)
		{
			Decimal z;
			if (formatProvider == null)
			{
				bool OK = Decimal.TryParse(s, out z);
				if (OK) { return z; }//if
				OK = Decimal.TryParse(s.Replace(cPoint, cComma), out z);
				if (OK) { return z; }//if
				OK = Decimal.TryParse(s.Replace(cComma, cPoint), out z);
				if (OK) { return z; }//if
				return def;
			}//if
			else
			{
				return Decimal.TryParse(s, NumberStyles.Number, formatProvider, out z) ? z : def;
			}//else
		}//function

		public static int parse(this string s, int def)
		{
			int z;
			return int.TryParse(s, out z) ? z : def;
		}//function

		/// <summary>
		/// split : двоеточие
		/// </summary>
		public static string[] splitColon(this string s)	{	return s.Split(cColon);	}//function
		/// <summary>
		/// split , запятая
		/// </summary>
		public static string[] splitComma(this string s) { return s.Split(cComma); }//function
		/// <summary>
		/// split ; точка с запятой
		/// </summary>
		public static string[] splitSemicolon(this string s) { return s.Split(cSemicolon); }//function
		/// <summary>
		/// split . точка
		/// </summary>
		public static string[] splitPoint(this string s) { return s.Split(cPoint); }//function
	}//class
}//ns

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDB
{
	public static class StringExtensions
	{
		public static string After(this string s, string Prefix) { return s.Substring(Prefix.Length); }//func
		//public static string FormatA(this string s, params object[] oo)		{ return string.Format(s, oo); }//func
		public static string Fmt(this string s, params object[] oo) { return string.Format(s, oo); }//func
		public static bool In(this string s, params string[] ss)		{return ss.Contains(s);	}//func

		public static string Before(this string s, string Suffix)
		{
			int i = s.IndexOf(Suffix);
			if (i > 0)
			{
				return s.Substring(0, i);
			}//if
			else
			{
				return string.Empty;
			}//else
		}//func

		

		public static string Path(this string s, params string[] args)
		{
			foreach (string path in args)
			{
				s = System.IO.Path.Combine(s, path);
			}//for
			return s;
		}//func
	}//class
}//ns

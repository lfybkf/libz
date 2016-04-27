using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB.Owin.Razor
{
	public static class WebExtensions
	{
		public static string tagIt(this string tag, string what, string clazz = null)
		{
			if (tag == null) { return string.Empty; }

			string result = clazz.isEmpty() ? "<{1}>{0}</{1}>".fmt(what, tag) : "<{1} class='{2}'>{0}</{1}>".fmt(what, tag, clazz);
			return result;
		}//function

		public static string tagThem(this string tag, params object[] what)
		{
			if (tag == null || what.Any() == false) { return string.Empty; }
			return string.Join(string.Empty, what.Select(z => "<{0}>{1}</{0}>".fmt(tag, z.ToString())));
		}//function

		public static string tagLink(this string what, string href, string clazz = null)
		{
			if (what == null)
				return string.Empty;

			string result = clazz.isEmpty() ? "<a href='{1}'>{0}</a>".fmt(what, href) : "<a href='{1}' class='{2}'>{0}</a>".fmt(what, href, clazz);
			return result;
		}//function

		public static string addSlash(this string s, params object[] args)
		{
			StringBuilder sb = new StringBuilder(s);
			args.forEach(o => sb.AppendFormat("/{0}", o.ToString()));
			return sb.ToString();
		}//function
	}//class
}//namespace

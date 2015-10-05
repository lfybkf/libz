using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB.Templating
{
	public static class Extensions
	{
		public static string fmt(this string s, params object[] oo) { return string.Format(s, oo); }//func

		public static string print<T>(this IEnumerable<T> source, Func<T, string> toString, string delimiter)
		{
			if (source == null)
				return string.Empty;

			return string.Join(delimiter, source.Select(z => toString(z)));
		}//function


		public static IEnumerable<T> forEachT<T>(this IEnumerable<T> source, Action<T> action)
		{
			if (source == null)
				return null;

			if (action == null)
				return source;

			foreach (var item in source)
				action(item);

			return source;
		}//function

		public static TResult withT<TSource, TResult>(this TSource source, Func<TSource, TResult> func)
		where TSource : class
		{
			if (source != default(TSource)) { return func(source); }//if
			else { return default(TResult); }//else
		}//function

		public static TResult withT<TSource, TResult>(this TSource source, Func<TSource, TResult> func, TResult defaultValue)
		where TSource : class
		{
			if (source != default(TSource)) { return func(source); }//if
			else { return defaultValue; }//else
		}//function

		public static TSource executeT<TSource>(this TSource source, Action<TSource> action) where TSource : class
		{
			if (source != default(TSource)) { action(source); } return source;
		}//function

		public static TValue getT<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
		{
			return dict.ContainsKey(key) ? dict[key] : default(TValue);
		}//function
	}//class
}//ns

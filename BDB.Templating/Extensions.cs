using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB.Templating
{
	/// <summary>
	/// класс расширений для шаблонов
	/// несмотря на то, что эти функции есть в BDB.Extensions.dll, все равно нужен, чтобы шаблоны могли ссылаться только на BDB.Bently.dll
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// массив Т в строку с разделителем
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="toString">функция, превращающая Т в строку</param>
		/// <param name="delimiter">разделитель</param>
		/// <returns></returns>
		public static string print<T>(this IEnumerable<T> source, Func<T, string> toString, string delimiter)
		{
			if (source == null)
				return string.Empty;

			return string.Join(delimiter, source.Select(z => toString(z)));
		}//function

		/// <summary>
		/// формат строки
		/// usage: "Value1={0}, Value2={1}".fmt(Value1, Value2)
		/// </summary>
		/// <param name="s"></param>
		/// <param name="oo"></param>
		/// <returns></returns>
		public static string fmt(this string s, params object[] oo) { return string.Format(s, oo); }//function


		public static IEnumerable<T> forEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			if (source == null)
				return null;

			if (action == null)
				return source;

			foreach (var item in source)
				action(item);

			return source;
		}//function

		/// <summary>
		/// вертает Value или default(TValue)
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static TValue get<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
		{
			return dict.ContainsKey(key) ? dict[key] : default(TValue);
		}//function

		/// <summary>
		/// вертает Value или defaultValue
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static TValue get<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
		{
			return dict.ContainsKey(key) ? dict[key] : defaultValue;
		}//function

	}//class
}//ns

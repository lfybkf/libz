using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BDB.Templating
{
	/// <summary>
	/// класс расширений для шаблонов
	/// несмотря на то, что эти функции есть в BDB.Extensions.dll
	/// , все равно нужен, чтобы шаблоны могли ссылаться только на BDB.Templating.dll
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

		public static bool notEmpty(this string s) { return !string.IsNullOrWhiteSpace(s); }
		public static bool isEmpty(this string s) { return string.IsNullOrWhiteSpace(s); }

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

		public static string midst(this string s, string Prefix, string Suffix)
		{
			int iPrefix = s.IndexOf(Prefix);
			int iSuffix = s.IndexOf(Suffix);
			if (iPrefix >= 0 && iSuffix > 0)
				return s.Substring(iPrefix + Prefix.Length, iSuffix - iPrefix - Prefix.Length);
			else if (iPrefix >= 0)
				return s.Substring(iPrefix + Prefix.Length);
			else if (iSuffix >= 0)
				return s.Substring(0, iSuffix);
			else
				return string.Empty;
		}//func


		#region fmtO


		public static string getPropertyValue(this object o, string name)
		{
			if (o == null) { return null; }
			if (o is IDictionary<string, string>)
			{
				var dict = (o as IDictionary<string, string>);
				return dict.ContainsKey(name) ? dict[name] : null;
			}//if
			Type t = o.GetType();
			PropertyInfo fi = t.GetProperty(name);
			if (fi == null)
			{
				return null;
			}//if

			return fi.GetValue(o).ToString();
		}//function

		public static string getMethodValue(this object o, string name, params object[] parameters)
		{
			Type t = o.GetType();
			MethodInfo mi = t.GetMethod(name);
			if (mi == null)
			{
				return null;
			}//if

			string result = null;
			try
			{
				object res = mi.Invoke(o, parameters);
				if (res != null) { result = res.ToString(); }
			}//try
			catch
			{

			}//catch
			return result;
		}//function

		//если эти символы в плейсхолдере, то это не он и его игнорируем
		private static char[] badCharsForPlaceholder = { ' ', '\t', '\n' };

		internal static IDictionary<int, string> getPlaceholders(this string s, char cL = '{', char cR = '}')
		{
			if (s.IndexOf(cL) < 0) { return null; }//if

			IDictionary<int, string> result = new Dictionary<int, string>();
			char c;
			int iL = -1;
			string ph;
			// "asd{ph}qwerty"
			for (int i = 0; i < s.Length; i++)
			{
				c = s[i];
				if (c == cL)
				{
					iL = i;//i=3
				}//if
				else if (c == cR && iL >= 0) //i=6
				{
					ph = s.Substring(iL, i - iL + 1);
					if (badCharsForPlaceholder.Any(z => ph.Contains(z)) == false)
					{ result.Add(iL, ph); }
					iL = -1;
				}//if
			}//for

			return result;
		}//func

		public static string fmto(this string s, params object[] oo)
		{
			//получаем список плейс-холдеров
			var phS = s.getPlaceholders();
			if (phS == null) { return s; }

			#region заполняем значения плейс-холдеров
			IDictionary<string, string> values = new Dictionary<string, string>();
			string ph;
			string method;
			string[] args;
			string value;
			int iL, iR; //номер скобок
			foreach (var item in phS.Values.Distinct())
			{
				ph = item.Substring(1, item.Length - 2);//2="{}".Length
				method = null;
				args = null;
				iL = ph.IndexOf('(');
				if (iL >= 0)
				{
					iR = ph.IndexOf(')');
					method = ph.Substring(0, iL);
					args = (iR > iL + 1) ? ph.Substring(iL + 1, iR - iL - 1).Split(',') : null;
				}//if

				//обходим каждый объект из входящего списка
				//вдруг у кого-то есть нужное свойство или метод
				foreach (object o in oo)
				{
					if (method != null)
					{
						value = o.getMethodValue(method, args);
					}//if
					else
					{
						value = o.getPropertyValue(ph);
					}//else

					//нашлось - записываем и дальше не смотрим
					if (value != null)
					{
						values.Add(item, value);
						break;
					}//if
				}//for
			}//for
			#endregion

			#region формируем строку-результат
			string sPh;
			StringBuilder sb = new StringBuilder();
			int iPast = 0;//end of previous placeholder
			foreach (int iPh in phS.Keys)
			{
				sb.Append(s.Substring(iPast, iPh - iPast)); //before Ph
				sPh = phS[iPh]; //"{name}"
				sb.Append(values.ContainsKey(sPh) ? values[sPh] : string.Empty);
				iPast = iPh + sPh.Length;
			}//for
			sb.Append(s.Substring(iPast));
			#endregion

			return sb.ToString();
		}//function
		#endregion
	}//class
}//ns

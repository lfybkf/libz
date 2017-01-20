using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	///<summary>IEnumerable IDictionary</summary> 
	public static class IEnumerableExtension
	{
		///<summary>for each</summary> 
		public static IEnumerable<T> forEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			if (source == null) { return null; }
			if (action == null) { return source; }
			foreach (var item in source) { action(item); }
			return source;
		}//function

		/// <summary>
		/// качельное перебирание последовательности
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="limit">количество переборов. Если 0, то =Count</param>
		/// <returns></returns>
		public static IEnumerable<T> sequenceSwing<T>(this IEnumerable<T> source, int limit = 0)
		{
			if (source == null) { yield break; }
			int max = source.Count();
			if (max == 1) { yield break; }
			int index = 0;
			if (limit == 0) { limit = max; }

			int direction = 1;
			for (int i = 0; i < limit; i++)
			{
				yield return source.ElementAt(index);

				if (index == max - 1) { direction = -1; }
				else if (index == 0) { direction = 1; }
				index += direction;
			}//for
		}//function

		/// <summary>
		/// цикличное перебирание последовательности
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="limit">количество переборов. Если 0, то =Count</param>
		/// <returns></returns>
		public static IEnumerable<T> sequenceCircle<T>(this IEnumerable<T> source, int limit = 0)
		{
			if (source == null) { yield break; }
			int max = source.Count();
			if (max == 1) { yield break; }
			int index = 0;
			if (limit == 0) { limit = max; }

			for (int i = 0; i < limit; i++)
			{
				yield return source.ElementAt(index);
				if (index == max - 1) { index = 0; }
				else { index++; }
			}//for
		}//function

		/// <summary>
		/// произвольное перебирание последовательности
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="limit">количество переборов. Если 0, то =Count</param>
		/// <returns></returns>
		public static IEnumerable<T> sequenceRandom<T>(this IEnumerable<T> source, int limit = 0)
		{
			if (source == null) { yield break; }
			int max = source.Count();
			if (max == 1) { yield break; }
			int index = 0;
			if (limit == 0) { limit = max; }

			Random random = new Random();
			for (int i = 0; i < limit; i++)
			{
				index = random.Next(0, max);
				yield return source.ElementAt(index);
			}//for
		}//function

		///<summary>проход всей последовательности через N со сдвигом</summary>
		public static IEnumerable<T> sequenceSkip<T>(this IEnumerable<T> source, int N)
		{
			if (source == null) { yield break; }
			if (N == 0) { yield break; }
			int count = source.Count();
			if (count == 1) { yield break; }
			int frags = (count / N) + 1;
			int index;
			for (int i = 0; i < N; i++)
			{
				for (int fi = 0; fi < frags; fi++)
				{
					index = fi * N + i;
					if (index < count) { yield return source.ElementAt(index); }
				}//for
			}//for
		}//function

		/// <summary>массив Т в строку с разделителем. Если функция не указана, то используется ToString (для строк - тождество)</summary>
		public static string print<T>(this IEnumerable<T> source, Func<T, string> toStr, string delimiter)
		{
			if (source == null) { return string.Empty; }
			if (toStr == null)
			{
				if (typeof(T) == typeof(string)) { return string.Join(delimiter, source.Select(z => z));	}//if
				else { return string.Join(delimiter, source.Select(z => z.ToString())); }//else
			}//if
			return string.Join(delimiter, source.Select(z => toStr(z)));
		}//function

		///<summary>is empty</summary> 
		public static bool isEmpty<T>(this IEnumerable<T> list) => list == null || list.Any() == false;
		///<summary>not empty</summary> 
		public static bool notEmpty<T>(this IEnumerable<T> list) => list != null && list.Any();


		/// <summary>вертает Value или default(TValue)</summary>
		public static TValue get<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) => dict.ContainsKey(key) ? dict[key] : default(TValue);
		/// <summary>вертает Value или defaultValue</summary>
		public static TValue get<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue def) => dict.ContainsKey(key) ? dict[key] : def;
		///<summary>Dictionary is empty</summary> 
		public static bool isEmpty<T, V>(this IDictionary<T, V> dict) => dict == null || dict.Any() == false;
		///<summary>Dictionary not empty</summary> 
		public static bool notEmpty<T, V>(this IDictionary<T, V> dict) => dict != null && dict.Any();
		///<summary>брать, пока не выполнено</summary>
		public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> list, Func<T, bool> predicate) => list.TakeWhile(z => !predicate(z));

		///<summary>Берет пока условие невыполнено последний раз. Если ни разу, то возвращает все.
		///{1,2,3,4,3,4,5}.TakeUntilLast(4) = {1,2,3,4,3}</summary>
		public static IEnumerable<T> TakeUntilLast<T>(this IEnumerable<T> list, Func<T, bool> predicate)
		{
			if (list.isEmpty()) { return list; }
			int index = -1;
			int count = list.Count();

			for (int i = count-1; i >= 0; i--)
			{
				if (predicate(list.ElementAt(i))) { index = i; break; }
			}//for

			if (index >= 0)
			{
				return list.Take(index);
			}//if
			else
			{
				return list;
			}//else
		}//function

		///<summary>shuffle</summary>
		public static void shuffle<T>(this T[] array)
		{
			Random rnd = new Random();
			int n = array.Length;
			while (n > 1)
			{
				int k = rnd.Next(n--);
				T temp = array[n];
				array[n] = array[k];
				array[k] = temp;
			}
		}//function

		///<summary>shuffle</summary>
		public static IEnumerable<T> orderByRandom<T>(this IEnumerable<T> list)
		{
			Random rnd = new Random();
			return list.OrderBy(z => rnd.Next());
		}//function
	}//class
}//ns

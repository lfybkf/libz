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

		///<summary>сортированные строки</summary>
		public static IEnumerable<string> ordered(this IEnumerable<string> list, bool forward = true) => forward ? list.OrderBy(s => s) : list.OrderByDescending(s=>s);

		///<summary>1.sequence(2,3,4) = {1,2,3,4}</summary>
		public static IEnumerable<T> sequence<T>(this T one, params T[] args) => Enumerable.Repeat(one, 1).Concat(args);
		///<summary>{1,2}.sequence(3,4) = {1,2,3,4}</summary>
		public static IEnumerable<T> sequence<T>(this IEnumerable<T> list, params T[] args) => list.Concat(args);

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

		/// <summary>цикличное перебирание последовательности</summary>
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
		public static IEnumerable<T> takeUntil<T>(this IEnumerable<T> list, Func<T, bool> predicate) => list.TakeWhile(z => !predicate(z));

		///<summary>Берет пока условие невыполнено последний раз. Если ни разу, то возвращает все.
		///{1,2,3,4,3,4,5}.TakeUntilLast(4) = {1,2,3,4,3}</summary>
		public static IEnumerable<T> takeUntilLast<T>(this IEnumerable<T> list, Func<T, bool> predicate)
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
		public static void shuffle<T>(this IList<T> list)
		{
			Random rnd = new Random();
			int n = list.Count;
			while (n > 1)
			{
				int k = rnd.Next(n--);
				T temp = list[n];
				list[n] = list[k];
				list[k] = temp;
			}
		}//function


		///<summary>shuffle</summary>
		public static IEnumerable<T> orderByRandom<T>(this IEnumerable<T> list)
		{
			Random rnd = new Random();
			return list.OrderBy(z => rnd.Next());
		}//function

		///<summary>distinct on function</summary>
		public static IEnumerable<T> distinctBy<T>(this IEnumerable<T> source, Func<T, T, bool> funcEquals, Func<T, int> funcHash = null)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			var comparer = new FuncComparer<T>(funcEquals, funcHash);
			return source.Distinct(comparer);
		}//function

		///<summary>distinct on property</summary>
		public static IEnumerable<TSource> distinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));

			var knownKeys = new HashSet<TKey>();
			foreach (var element in source)
			{
				if (knownKeys.Add(keySelector(element)))
				{
					yield return element;
				}//if
			}//for
		}//function


		///<summary>подсчет количества элементов по значениям keySelector. Пример: {"aaaaa", "b", "bb", "CC", "d", "e"}.CountBy(s => s.Length) = {5,1}{2,2}{1,3}</summary>
		public static IDictionary<TKey, int> countBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
		{
			Dictionary<TKey, int> result = new Dictionary<TKey, int>();
			if (source == null) return result;

			TKey key;
			foreach (var item in source)
			{
				key = keySelector(item);
				if (result.ContainsKey(key))
				{
					result[key] = result[key] + 1;
				}//if
				else
				{
					result[key] = 1;
				}//else
			}//for
			return result;
		}//function

		///<summary>max by property</summary>
		public static TSource maxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (source.Any() == false) throw new InvalidOperationException(nameof(source));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			IComparer<TKey> comparer = Comparer<TKey>.Default;

			using (var sourceIterator = source.GetEnumerator())
			{
				sourceIterator.MoveNext();
				var max = sourceIterator.Current;
				var maxValue = selector(max);
				while (sourceIterator.MoveNext())
				{
					var current = sourceIterator.Current;
					var currentValue = selector(current);
					if (comparer.Compare(currentValue, maxValue) > 0)
					{
						max = current;
						maxValue = currentValue;
					}//if
				}//while
				return max;
			}//using
		}//function

		///<summary>min by property</summary>
		public static TSource minBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (source.Any() == false) throw new InvalidOperationException(nameof(source));
			if (selector == null) throw new ArgumentNullException(nameof(selector));
			IComparer<TKey> comparer = Comparer<TKey>.Default;

			using (var sourceIterator = source.GetEnumerator())
			{
				sourceIterator.MoveNext();
				var min = sourceIterator.Current;
				var minValue = selector(min);
				while (sourceIterator.MoveNext())
				{
					var current = sourceIterator.Current;
					var currentValue = selector(current);
					if (comparer.Compare(currentValue, minValue) < 0)
					{
						min = current;
						minValue = currentValue;
					}//if
				}//while
				return min;
			}//using
		}//function

		///<summary>sumBy</summary>
		///<example>
		///<code>
		///{"aa", "abb", "ccc"}.sumBy(s => s.StartsWith("a"), s => s.Length)
		///</code>
		///</example>
		public static Dictionary<TKey, int> sumBy<T, TKey>(this IEnumerable<T> list, Func<T, TKey> funcKey, Func<T, int> funcValue)
		{
			var result = new Dictionary<TKey, int>();
			if (list == null || list.Any() == false) { return result; }

			TKey key;
			foreach (var item in list)
			{
				key = funcKey(item);
				result[key] = result.ContainsKey(key) ? result[key] + funcValue(item) : funcValue(item);
			}//for

			return result;
		}//function

		///<summary>sumBy</summary> 
		public static Dictionary<TKey, double> sumBy<T, TKey>(this IEnumerable<T> list, Func<T, TKey> funcKey, Func<T, double> funcValue)
		{
			var result = new Dictionary<TKey, double>();
			if (list == null || list.Any() == false) { return result; }

			TKey key;
			foreach (var item in list)
			{
				key = funcKey(item);
				result[key] = result.ContainsKey(key) ? result[key] + funcValue(item) : funcValue(item);
			}//for

			return result;
		}//function

		///<summary>Сравнить без учета длины. "abcd" coincide "abc".</summary>
		public static bool isCoincide<T>(this IEnumerable<T> list, IEnumerable<T> others, Func<T, T, bool> equals = null)
		{
			if (equals == null) { equals = EqualityComparer<T>.Default.Equals; }
			IEnumerator<T> me = list.GetEnumerator();
			IEnumerator<T> you = others.GetEnumerator();
			
			bool res = true;
			while (me.MoveNext() && you.MoveNext())
			{
				if (equals(me.Current, you.Current) == false)
				{
					res = false;
					break;
				}//if
			}//while
			return res;
		}//function

		///<summary>содержит массив</summary>
		public static bool contains<T>(this IEnumerable<T> list, IEnumerable<T> what, Func<T, T, bool> equals = null)
		{
			if (list.isEmpty()) { return false; }
			if (what.isEmpty()) { return false; }
			if (equals == null) { equals = EqualityComparer<T>.Default.Equals; }
			IEnumerator<T> me = list.GetEnumerator();
			IEnumerator<T> you = null;

			bool res = false;
			bool isMatching = false;

			while (me.MoveNext())
			{
				if (isMatching == false)
				{
					if (equals(me.Current, what.First()))
					{
						you = what.GetEnumerator();
						you.MoveNext();
						isMatching = true;
						res = true;
					}//if
					continue;
				}//if

				if (you.MoveNext())
				{
					if (equals(me.Current, you.Current) == false)
					{
						isMatching = false;
						res = false;
					}//if
				}//if
				else
				{
					//есть одно вхождение - дальше не ищем
					if (res == true) { break; }//if
				}//else
			}//while

			return res;
		}//fun

	}//class
}//ns

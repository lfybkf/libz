using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	public static class IEnumerableExtension
	{
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

				if (index == max - 1)			{	direction = -1;}
				else if (index == 0)				{ direction = 1; }
			index += direction;
			}//for
		}//function

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
	}//class
}//ns

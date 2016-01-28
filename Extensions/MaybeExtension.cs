using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	public static class MaybeExtension
	{
		/// <summary> 
		/// Позволяет работать с объектом <paramref name="source"/> , даже если он null
		/// Если null, то возвращает default(<typeparamref name="TResult"/>)
		/// Если не null, то возвращает результат функции(<paramref name="func"/>)
		/// </summary> 
		/// <typeparam name="TSource">Тип объекта</typeparam> 
		/// <typeparam name="TResult">Тип результата</typeparam> 
		/// <param name="source">Объект</param> 
		/// <param name="func">Функция, дающая результат</param> 
		/// <returns>результат</returns> 
		public static TResult with<TSource, TResult>(this TSource source, Func<TSource, TResult> func)
		where TSource : class
		{
			if (source != default(TSource)) 	{				return func(source);			}//if
			else		{		return default(TResult);		}//else
		}//function

		public static TResult with<TSource, TResult>(this TSource source, Func<TSource, TResult> func, TResult defaultValue)
		where TSource : class
		{
			if (source != default(TSource))		{		return func(source);		}//if
			else	{		return defaultValue;		}//else
		}//function

		public static void execute<TSource>(this Action<TSource> action, TSource source)
		{
			if (action != null) {action(source);}
		}//function

		public static void execute<T1, T2>(this Action<T1,T2> action, T1 arg1, T2 arg2)
		{
			if (action != null) { action(arg1, arg2); }
		}//function

	}//class
}//ns

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

		public static TSource execute<TSource>(this TSource source, Action<TSource> action)	where TSource : class		
		{	
			if (source != default(TSource))	{action(source);}		return source; 
		}//function

	}//class
}//ns

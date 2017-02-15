using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	///<summary>универсальный компарер</summary>
	public class FuncComparer<T> : IEqualityComparer<T>
	{
		private readonly Func<T, T, bool> _funcEquals;
		private readonly Func<T, int> _funcHash;

		private int fakeGetHashCode(T obj) => 0;

		///<summary>ctor</summary>
		public FuncComparer(Func<T, T, bool> funcEquals, Func<T, int> funcHash = null)
		{
			_funcEquals = funcEquals;
			_funcHash = funcHash ?? fakeGetHashCode;
		}

		///<summary>Equals</summary>
		public bool Equals(T x, T y) => _funcEquals(x, y);
		///<summary>GetHashCode</summary>
		public int GetHashCode(T obj) => _funcHash(obj);
	}//class
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	public static class TypeExtension
	{
		public static bool isNullable(this Type t)
		{
			if (t.IsGenericType)
				return t.GetGenericTypeDefinition() == typeof(Nullable<>);
			else
				return false;
		}//function

		public static Type getTypeUnderNullable(this Type t)
		{
			return t.isNullable() ? Nullable.GetUnderlyingType(t) : t;
		}//function
	}//class
}//ns

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	public static class TypeExtension
	{
		static Dictionary<Type, DbType> typeMap = new Dictionary<Type, DbType>() 
		{
			{typeof(byte), DbType.Byte}, 
			{typeof(sbyte), DbType.SByte}, 
			{typeof(short), DbType.Int16}, 
			{typeof(ushort), DbType.UInt16}, 
			{typeof(int), DbType.Int32}, 
			{typeof(uint), DbType.UInt32}, 
			{typeof(long), DbType.Int64}, 
			{typeof(ulong), DbType.UInt64}, 
			{typeof(float), DbType.Single}, 
			{typeof(double), DbType.Double}, 
			{typeof(decimal), DbType.Decimal}, 
			{typeof(bool), DbType.Boolean}, 
			{typeof(string), DbType.String}, 
			{typeof(char), DbType.StringFixedLength}, 
			{typeof(Guid), DbType.Guid}, 
			{typeof(DateTime), DbType.DateTime}, 
			{typeof(DateTimeOffset), DbType.DateTimeOffset}, 
			{typeof(byte[]), DbType.Binary}, 
			{typeof(byte?), DbType.Byte}, 
			{typeof(sbyte?), DbType.SByte}, 
			{typeof(short?), DbType.Int16}, 
			{typeof(ushort?), DbType.UInt16}, 
			{typeof(int?), DbType.Int32}, 
			{typeof(uint?), DbType.UInt32}, 
			{typeof(long?), DbType.Int64}, 
			{typeof(ulong?), DbType.UInt64}, 
			{typeof(float?), DbType.Single}, 
			{typeof(double?), DbType.Double}, 
			{typeof(decimal?), DbType.Decimal}, 
			{typeof(bool?), DbType.Boolean}, 
			{typeof(char?), DbType.StringFixedLength}, 
			{typeof(Guid?), DbType.Guid}, 
			{typeof(DateTime?), DbType.DateTime}, 
			{typeof(DateTimeOffset?), DbType.DateTimeOffset}, 
		};

		public static DbType getDbType(this Type t)		{			return typeMap.get(t);		}//function
		public static bool isNullable(this Type t)		{			return (t.IsGenericType) ? (t.GetGenericTypeDefinition() == typeof(Nullable<>)) : false;}//function
		public static Type getTypeUnderNullable(this Type t)	{	return t.isNullable() ? Nullable.GetUnderlyingType(t) : t;}//function

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
			if (o == null) { return null; }
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
	}//class
}//ns

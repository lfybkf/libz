using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BDB.Doubt
{
	public class RegistrySQL
	{
		class RegistryItem
		{
			public Type type;
			public IStoreSQL store;
			public string Table;
			public Field[] fields;
		}//class

		class Field
		{
			public string Name;
			public Type type;
			public SqlTypes sqlType;
			public string DefaultValue;
		}//class

		private static IList<RegistryItem> list = new List<RegistryItem>(100);
		public static IStoreSQL defaultStore;

		private static RegistryItem get(Type type)		{		return list.FirstOrDefault(z => z.type == type);	}//function
		public static IStoreSQL Store(Type type) { return get(type).with(i => i.store); }//function
		public static IStoreSQL Store<T>() { return get(typeof(T)).with(i => i.store); }//function

		public static bool AddType(Type type, IStoreSQL store = null) 
		{
			RegistryItem item = get(type);
			if (item != null) { return false; }
			
			item = new RegistryItem();
			item.type = type;
			item.store = store ?? defaultStore;

			Attribute attr = Attribute.GetCustomAttributes(type).FirstOrDefault(z => z.GetType().Equals(typeof(TableAttribute)));
			item.Table = (attr as TableAttribute).with(z => z.Value) ?? type.Name;

			list.Add(item);
			return true;
		}//function

	}//class
}//ns

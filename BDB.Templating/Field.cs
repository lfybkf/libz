#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BDB.Templating
{
	public class Field: Attrz
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public string Ref { get; set; }
		public string Default { get; set; }
		public bool IsNullable { get; set; }
		public bool NotInsert { get; set; }
		public bool NotUpdate { get; set; }
		
		static string[] value_types = { "bool", "datetime", "decimal", "int", "long", "float", "double" };

		internal override void Read(XElement src)
		{
			Name = Get(R.Name);
			Type = Get(R.Type);
			Ref = Get(R.Ref);
			Default = Get(R.Default);
			IsNullable = Get(R.Null) != null;
			NotInsert = Get(R.NotInsert) != null;
			NotUpdate = Get(R.NotUpdate) != null;
		}//function

		public static string getTypeDB(string Type)
		{
			string result = "DbType.";
			if (Type == "bool") { result += "Boolean"; }
			else if (Type == "decimal") { result += "Decimal"; }
			else if (Type == "int") { result += "Int32"; }
			else if (Type == "long") { result += "Int64"; }
			else if (Type == "datetime") { result += "DateTime"; }
			else if (Type == "string") { result += "String"; }
			else if (Type == "float") { result += "Double"; }
			else if (Type == "double") { result += "Double"; }

			return result;
		}//function

		public static string getTypeReader(string Type)
		{
			string result;
			if (Type == "bool") { result = "Boolean"; }
			else if (Type == "decimal") { result = "Decimal"; }
			else if (Type == "int") { result = "Int32"; }
			else if (Type == "long") { result = "Int64"; }
			else if (Type == "datetime") { result = "DateTime"; }
			else if (Type == "string") { result = "String"; }
			else if (Type == "float") { result = "Float"; }
			else if (Type == "double") { result = "Double"; }
			else { result = string.Empty; }
			return result;
		}//function

		public static string getTypeCS(string Type, bool IsNullable = false)
		{
			string result;
			//set type
			if (Type == "datetime") { result = "DateTime"; } else { result = Type; }
			//add null if need
			if (value_types.Contains(Type) && IsNullable) { result += "?"; }
			return result;
		}//function

		public string TypeDB			{ get {	return getTypeDB(Type);		}	}
		public string TypeReader	{ get { return getTypeReader(Type); } }
		public string TypeCS			{ get {	return getTypeCS(Type, IsNullable); }	}
	}//class
}//ns

#pragma warning restore 1591
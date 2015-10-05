using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BDB.Templating
{
	public class Field
	{
		public string Name;
		public string Type;
		public string Ref;
		public bool IsNullable;
		public bool NotInsert;
		public bool NotUpdate;
		
		private Dictionary<string, string> attrS = new Dictionary<string, string>();
		public bool Has(string key) { return attrS.ContainsKey(key); }
		public bool Has(string key, string value) { return attrS.getT(key) == value; }
		public bool HasNot(string key) { return !attrS.ContainsKey(key); }
		public bool HasNot(string key, string value) { return attrS.getT(key) != value; }

		static string[] date_types = {"date", "time", "datetime"};
		static string[] value_types = { "bool", "decimal", "int", "long"};

		public Field Read(XElement src)
		{
			foreach (XAttribute attr in src.Attributes()) { attrS.Add(attr.Name.LocalName, attr.Value); }//for

			Name = attrS.getT(R.NAME);
			Type = attrS.getT(R.TYPE);
			Ref = attrS.getT(R.REF);
			IsNullable = attrS.getT(R.NULL) != null;
			NotInsert = attrS.getT("NotInsert") != null;
			NotUpdate = attrS.getT("NotUpdate") != null;
			return this;
		}//function

		public static string getTypeDB(string Type)
		{
			string result = "DbType.";
			if (Type == "bool") { result += "Boolean"; }
			else if (Type == "decimal") { result += "Decimal"; }
			else if (Type == "int") { result += "Int32"; }
			else if (Type == "long") { result += "Int64"; }
			else if (Type == "date") { result += "Date"; }
			else if (Type == "datetime") { result += "DateTime"; }
			else if (Type == "time") { result += "Time"; }
			else if (Type == "string") { result += "String"; }

			return result;
		}//function

		public static string getTypeReader(string Type)
		{
			string result;
			if (Type == "bool") { result = "Boolean"; }
			else if (Type == "decimal") { result = "Decimal"; }
			else if (Type == "int") { result = "Int32"; }
			else if (Type == "long") { result = "Int64"; }
			else if (Type == "date") { result = "DateTime"; }
			else if (Type == "datetime") { result = "DateTime"; }
			else if (Type == "time") { result = "DateTime"; }
			else if (Type == "string") { result = "String"; }
			else { result = string.Empty; }
			return result;
		}//function

		public static string getTypeCS(string Type, bool IsNullable = false)
		{
			string result = Type;
			if (date_types.Contains(Type)) { result = "DateTime"; }
			if (date_types.Contains(Type) && IsNullable) { result += "?"; }
			if (value_types.Contains(Type) && IsNullable) { result += "?"; }
			return result;
		}//function

		public string TypeDB			{ get {	return getTypeDB(Type);		}	}
		public string TypeReader	{ get { return getTypeReader(Type); } }
		public string TypeCS			{ get {	return getTypeCS(Type, IsNullable); }	}


		public string zReadField(string varDataReader = "ddr")
		{
			string opRead = "{0} = {1}.Get{2}({1}Ordinal);"
				.fmt(Name, varDataReader, getTypeReader(Type));
			string opReadWithNull = "if ({1}.IsDBNull({1}Ordinal)) {{ {0}=null; }} else {{ {0} = {1}.Get{2}({1}Ordinal); }}"
				.fmt(Name, varDataReader, getTypeReader(Type));
			//if (ddr.IsDBNull(ddrOrdinal)) { Amount = null; } else { Amount = ddr.GetInt32(ddrOrdinal); }
			StringBuilder sb = new StringBuilder();
			sb.Append("{1}Ordinal = {1}.GetOrdinal(\"{0}\"); ".fmt(Name, varDataReader));
			if (IsNullable) { sb.Append(opReadWithNull); }//if
			else { sb.Append(opRead); }//else
			return sb.ToString();
		}//function

	}//class
}//ns

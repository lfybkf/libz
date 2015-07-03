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
		public bool Null;
		public bool NotInsert;
		public bool NotUpdate;

		static string[] date_types = {"date", "time", "datetime"};
		static string[] value_types = { "bool", "decimal", "int", "long"};

		public Field Read(XElement src)
		{
			Name = src.Attribute(R.NAME) != null ? src.Attribute(R.NAME).Value : null;
			Type = src.Attribute(R.TYPE) != null ? src.Attribute(R.TYPE).Value : null;
			Ref = src.Attribute(R.REF) != null ? src.Attribute(R.REF).Value : null;
			Null = src.Attribute(R.NULL) != null;
			NotInsert = src.Attribute("NotInsert") != null;
			NotUpdate = src.Attribute("NotInsert") != null;
			return this;
		}//function

		public string TypeCS { get {
			string result = Type;
			if (date_types.Contains(Type)) { result = "DateTime"; }
			if (date_types.Contains(Type) && Null) { result += "?"; }
			if (value_types.Contains(Type) && Null) { result += "?"; }
			return result;
		} }


		public static string getDbType(string Type)
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

		public string DbType { get {			return getDbType(Type);		}	}
	}//class
}//ns

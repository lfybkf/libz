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
		public string Name;
		public string Type;
		public string Ref;
		public bool IsNullable;
		public bool NotInsert;
		public bool NotUpdate;
		
		static string[] value_types = { "bool", "datetime", "decimal", "int", "long", "float", "double" };

		public Field Read(XElement src)
		{
			Fill(src.Attributes());

			Name = Get(R.NAME);
			Type = Get(R.TYPE);
			Ref = Get(R.REF);
			IsNullable = Get(R.NULL) != null;
			NotInsert = Get(R.NotInsert) != null;
			NotUpdate = Get(R.NotUpdate) != null;
			return this;
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

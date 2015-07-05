using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BDB.Templating
{
	public class Obj
	{
		public static string Namespace = "BDB";
		public static string TablePrefix = string.Empty;

		public string Name;
		public List<Field> fields = new List<Field>();

		private Dictionary<string, string> attrS = new Dictionary<string, string>();
		public bool Has(string key) { return attrS.ContainsKey(key); }
		public bool Has(string key, string value) { return attrS.get(key) == value; }
		public bool HasNot(string key) { return !attrS.ContainsKey(key); }
		public bool HasNot(string key, string value) { return attrS.get(key) != value; }

		internal string _Table = null;
		public string Table { get { return TablePrefix + (_Table ?? Name); } }

		private string _IDfield = null;
		public string IDfield { get { return _IDfield ?? "ID"; } }
		private string _IDtype = null;
		public string IDtype { get { return _IDtype ?? "long"; } }

		public Obj Read(XElement src)
		{
			foreach (XAttribute attr in src.Attributes()) { attrS.Add(attr.Name.LocalName, attr.Value); }//for
			Name = attrS.get(R.NAME);
			_Table = attrS.get(R.TABLE);
			_IDfield = attrS.get("IDfield");
			_IDtype = attrS.get("IDtype");
			
			Field item = null;
			foreach (var xField in src.Elements(R.FIELD))
			{
				item = new Field();
				item.Read(xField);
				fields.Add(item);
			}//for
			return this;
		}//constructor

		public string InsertFields 
			{ get { 
				return fields.Where(f => !f.NotInsert).Select(f => f.Name).Aggregate((s1, s2) => "{0}, {1}".fmt(s1,s2)); 
			} }
		public string InsertParams 
			{ get { 
				return fields.Where(f => !f.NotInsert).Select(f => "@" + f.Name).Aggregate((s1, s2) => "{0}, {1}".fmt(s1, s2)); 
			} }
		public string SetsUpdate 
			{ get { 
				return fields.Where(f => !f.NotUpdate)
					.Select(f => "{0}=@{0}".fmt(f.Name)).Aggregate((s1, s2) => "{0}, {1}".fmt(s1, s2)); 
			} }
		public string SelectFields 
			{ get { 
				return IDfield + R.Z + fields.Select(f => f.Name).Aggregate((s1, s2) => "{0}, {1}".fmt(s1, s2)); 
			} }
		public string WhereID { get { return "{0} = @ID".fmt(IDfield); } }
		
		public string ReadID(string varReader = "ddr") 
		{
			string opOrdinal = "{0}Ordinal = {0}.GetOrdinal(\"{1}\");".fmt(varReader, IDfield);
			string opRead = "ID = {0}.Get{1}({0}Ordinal);".fmt(varReader, Field.getDbReaderType(IDtype));
			return opOrdinal + opRead;
		}

		public string sqlInsert { get { return "INSERT INTO {0} ({1}) VALUES ({2})".fmt(Table, InsertFields, InsertParams); } }
		public string sqlUpdate { get { return "UPDATE {0} SET {1} WHERE {2}".fmt(Table, SetsUpdate, WhereID); } }
		public string sqlDelete { get { return "DELETE FROM {0} WHERE {1}".fmt(Table, WhereID); } }
		public string sqlLoad { get { return "SELECT * FROM {0} WHERE {1}".fmt(Table, WhereID); } }
		public string sqlLoadForUpdate { get { return "SELECT * FROM {0} WITH(updlock) WHERE {1}".fmt(Table, WhereID); } }
		public string sqlSelect { get { return "SELECT * FROM {0} WHERE ".fmt(Table); } }

		public static string XmlDTD()
		{
return @"	
<!DOCTYPE ROOT [ 
	<!ELEMENT ROOT (Object+)>
	<!ELEMENT Object (Field+)>
		<!ATTLIST Object Name ID #REQUIRED>
		<!ATTLIST Object Table CDATA #IMPLIED>
    <!ATTLIST Object IDfield CDATA #IMPLIED>
    <!ATTLIST Object IDtype (int|long) #IMPLIED>
	<!ELEMENT Field (#PCDATA)>
		<!ATTLIST Field Name CDATA #REQUIRED>
		<!ATTLIST Field Type (bool|date|datetime|decimal|int|long|string|time) #REQUIRED>
		<!ATTLIST Field Null (true) #IMPLIED>
		<!ATTLIST Field Ref CDATA #IMPLIED>
	<!ELEMENT Comment (#PCDATA)>
]>";

		}//function
	}//class
}//ns

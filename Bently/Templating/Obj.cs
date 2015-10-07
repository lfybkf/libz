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
//		public static string defaultNamespace = "BDB";
		public static string defaultIDname = "ID";
		public static string defaultIDtype = "long";
		public static string TablePrefix = string.Empty;

		public string Name;
		public List<Field> fields = new List<Field>();

		private Dictionary<string, string> attrS = new Dictionary<string, string>();
		public bool Has(string key) { return attrS.ContainsKey(key); }
		public bool Has(string key, string value) { return attrS.getT(key) == value; }
		public bool HasNot(string key) { return !attrS.ContainsKey(key); }
		public bool HasNot(string key, string value) { return attrS.getT(key) != value; }

		internal string _Table = null;
		public string Table { get { return TablePrefix + (_Table ?? Name); } }

		private string _IDname = null;
		public string IDname { get { return _IDname ?? defaultIDname; } }
		private string _IDtype = null;
		public string IDtype { get { return _IDtype ?? defaultIDtype; } }

		public Obj Read(XElement src)
		{
			foreach (XAttribute attr in src.Attributes()) { attrS.Add(attr.Name.LocalName, attr.Value); }//for
			Name = attrS.getT(R.NAME);
			_Table = attrS.getT(R.TABLE);
			_IDname = attrS.getT("IDname");
			_IDtype = attrS.getT("IDtype");
			
			Field item = null;
			foreach (var xField in src.Elements(R.FIELD))
			{
				item = new Field();
				item.Read(xField);
				fields.Add(item);
			}//for
			return this;
		}//constructor

		public static string XmlDTD()
		{
			return @"	
<!DOCTYPE ROOT [ 
	<!ELEMENT ROOT (Object+)>
	<!ELEMENT Object (Field+)>
		<!ATTLIST Object Name ID #REQUIRED>
		<!ATTLIST Object Table CDATA #IMPLIED>
    <!ATTLIST Object IDname CDATA #IMPLIED>
    <!ATTLIST Object IDtype (int|long) #IMPLIED>
	<!ELEMENT Field (#PCDATA)>
		<!ATTLIST Field Name CDATA #REQUIRED>
		<!ATTLIST Field Type (bool|datetime|decimal|int|long|string) #REQUIRED>
		<!ATTLIST Field Null (true) #IMPLIED>
		<!ATTLIST Field Ref CDATA #IMPLIED>
	<!ELEMENT Comment (#PCDATA)>
]>";
		}//function

	}//class
}//ns

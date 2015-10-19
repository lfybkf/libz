using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BDB;

namespace BDB.Templating
{
	public class Obj: Attrz
	{
		public static string defaultIDname = "ID";
		public static string defaultIDtype = "long";
		public static string TablePrefix = string.Empty;

		public string Name;
		public List<Field> fields = new List<Field>();

		internal string _Table = null;
		public string Table { get { return TablePrefix + (_Table ?? Name); } }

		private string _IDname = null;
		public string IDname { get { return _IDname ?? defaultIDname; } }
		private string _IDtype = null;
		public string IDtype { get { return _IDtype ?? defaultIDtype; } }

		public Obj Read(XElement src)
		{
			Fill(src.Attributes());
			Name = Get(R.NAME);
			_Table = Get(R.TABLE);
			_IDname = Get("IDname");
			_IDtype = Get("IDtype");
			
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
		<!ATTLIST Field Type (bool|datetime|decimal|int|long|string|float|double) #REQUIRED>
		<!ATTLIST Field Null (true) #IMPLIED>
		<!ATTLIST Field Ref CDATA #IMPLIED>
	<!ELEMENT Comment (#PCDATA)>
]>";
		}//function

	}//class
}//ns

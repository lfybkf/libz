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

		internal string _Table = null;
		public string Table { get { return TablePrefix + (_Table ?? Name); } }

		private string _IDfield = null;
		public string IDfield { get { return _IDfield ?? "ID"; } }
		private string _IDtype = null;
		public string IDtype { get { return _IDtype ?? "long"; } }

		public Obj Read(XElement src)
		{
			Name = src.Attribute(R.NAME).Value;
			_Table = src.Attribute(R.TABLE).with(a => a.Value);
			_IDfield = src.Attribute("IDfield").with(a => a.Value);
			_IDtype = src.Attribute("IDtype").with(a => a.Value);
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
return @"	<!DOCTYPE ROOT [ 
	<!ELEMENT ROOT (Object+)>
	<!ELEMENT Object (Field+)>
		<!ATTLIST Object Name ID #REQUIRED>
		<!ATTLIST Object Table CDATA #IMPLIED>
    <!ATTLIST Object IDfield CDATA #IMPLIED>
    <!ATTLIST Object IDtype CDATA #IMPLIED>
	<!ELEMENT Field (#PCDATA)>
		<!ATTLIST Field Name CDATA #REQUIRED>
		<!ATTLIST Field Type (bool|date|datetime|decimal|int|long|string|time|uint) #REQUIRED>
		<!ATTLIST Field Null (true) #IMPLIED>
		<!ATTLIST Field Ref CDATA #IMPLIED>
	<!ELEMENT Comment (#PCDATA)>
]>";

		}//function
	}//class
}//ns

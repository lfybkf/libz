using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BDB.Templating
{
	public class Machine: Attrz
	{
		private string Name;
		public string Info;
		private List<State> states = new List<State>();
		private List<Transition> transitions = new List<Transition>();
		private List<Act> acts = new List<Act>();
		private List<Check> checks = new List<Check>();
		private List<Device> devices = new List<Device>();

		public override void Read(XElement src)
		{
			fillContentFromAttrs(src);
			Name = Get(R.NAME);
			Info = Get(R.INFO, string.Empty);

			FillListFromXlist<State>(states, src.Elements(R.STATE));
			FillListFromXlist<Transition>(transitions, src.Elements(R.TRANSITION));
			FillListFromXlist<Act>(acts, src.Elements(R.ACT));
			FillListFromXlist<Check>(checks, src.Elements(R.CHECK));
			FillListFromXlist<Device>(devices, src.Elements(R.DEVICE));
		}//function

		public void Validate(Action<string> print)
		{ 

		}//function

		public static string XmlDTD()
		{
			return @"	
<!DOCTYPE ROOT [ 
	<!ELEMENT ROOT (Machine+)>
	<!ELEMENT Machine (State+, Transition+, Check*, Action*, Device*, Comment*)>
		<!ATTLIST Machine Name ID #REQUIRED>
		<!ATTLIST Machine Info CDATA #IMPLIED>
	<!ELEMENT State (Comment*)>
		<!ATTLIST State Name CDATA #REQUIRED>
		<!ATTLIST State Enter CDATA #IMPLIED>
		<!ATTLIST State Exit CDATA #IMPLIED>
		<!ATTLIST State Info CDATA #IMPLIED>
	<!ELEMENT Transition (Check*, Action*, Comment*)>
		<!ATTLIST Transition Name CDATA #REQUIRED>
		<!ATTLIST Transition From CDATA #REQUIRED>
		<!ATTLIST Transition To CDATA #REQUIRED>
		<!ATTLIST Transition Info CDATA #IMPLIED>
	<!ELEMENT Check (Comment*)>
		<!ATTLIST Check Name CDATA #REQUIRED>
		<!ATTLIST Check Info CDATA #IMPLIED>
	<!ELEMENT Action (Comment*)>
		<!ATTLIST Action Name CDATA #REQUIRED>
		<!ATTLIST Action Info CDATA #IMPLIED>
	<!ELEMENT Device (Comment*)>
		<!ATTLIST Device Name CDATA #REQUIRED>
		<!ATTLIST Device Type CDATA #REQUIRED>
		<!ATTLIST Device Info CDATA #IMPLIED>
	<!ELEMENT Comment (#PCDATA)>
		<!ATTLIST Comment Author CDATA #REQUIRED>
]>
";
		}//function
	}//class
}//ns

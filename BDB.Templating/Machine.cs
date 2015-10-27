using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

[assembly: InternalsVisibleTo("UnitTest")]
namespace BDB.Templating
{
	public class Machine: AttrzME
	{
		private List<State> _states = new List<State>();
		private List<Transition> _transitions = new List<Transition>();
		private List<Act> _acts = new List<Act>();
		private List<Check> _checks = new List<Check>();
		private List<Device> _devices = new List<Device>();
		private List<Push> _pushes = new List<Push>();

		public IEnumerable<State> states { get { return _states; } }
		public IEnumerable<Transition> transitions { get { return _transitions; } }
		public IEnumerable<Act> acts { get { return _acts; } }
		public IEnumerable<Check> checks { get { return _checks; } }
		public IEnumerable<Device> devices { get { return _devices; } }
		public IEnumerable<Push> pushes { get { return _pushes; } }

		internal override void Read(XElement src)
		{
			base.Read(src);

			FillListFromXlist<State>(_states, src.Elements(R.STATE));
			FillListFromXlist<Transition>(_transitions, src.Elements(R.TRANSITION));
			FillListFromXlist<Act>(_acts, src.Elements(R.ACT));
			FillListFromXlist<Check>(_checks, src.Elements(R.CHECK));
			FillListFromXlist<Device>(_devices, src.Elements(R.DEVICE));
			FillListFromXlist<Push>(_pushes, src.Elements(R.PUSH));

			states.forEach(z => z.MachineName = Name);
			transitions.forEach(z => z.MachineName = Name);
			acts.forEach(z => z.MachineName = Name);
			checks.forEach(z => z.MachineName = Name);
			devices.forEach(z => z.MachineName = Name);
			pushes.forEach(z => z.MachineName = Name);
		}//function

		internal bool HasState(string name) { return states.Any(z => z.Name == name); }
		internal bool HasCheck(string name) { return checks.Any(z => z.Name == name); }
		internal bool HasAct(string name) { return acts.Any(z => z.Name == name); }
		internal bool HasPush(string name) { return pushes.Any(z => z.Name == name); }

		public override bool Validate()
		{
			Action<AttrzME> validate = (z) =>
				{
					z.Validate();
					addErrors(z.errors);
					addWarnings(z.warnings);
				};

			states.forEach(validate);
			transitions.forEach(validate);
			acts.forEach(validate);
			checks.forEach(validate);
			devices.forEach(validate);
			pushes.forEach(validate);

			return hasErrors;
		}//function

		public static string XmlDTD()
		{
			return @"	
<!DOCTYPE ROOT [ 
	<!ELEMENT ROOT (Machine+)>
	<!ELEMENT Machine (State+, Transition+, Push+, Check*, Act*, Device*, Comment*)>
		<!ATTLIST Machine Name ID #REQUIRED>
		<!ATTLIST Machine Info CDATA #IMPLIED>
	<!ELEMENT State (Comment*)>
		<!ATTLIST State Name ID #REQUIRED>
		<!ATTLIST State Enter CDATA #IMPLIED>
		<!ATTLIST State Exit CDATA #IMPLIED>
		<!ATTLIST State Info CDATA #IMPLIED>
	<!ELEMENT Transition (Comment*)>
		<!ATTLIST Transition From CDATA #REQUIRED>
		<!ATTLIST Transition To CDATA #REQUIRED>
		<!ATTLIST Transition Pushes CDATA #REQUIRED>
		<!ATTLIST Transition Checks CDATA #IMPLIED>
		<!ATTLIST Transition Acts CDATA #IMPLIED>
		<!ATTLIST Transition Info CDATA #IMPLIED>
	<!ELEMENT Check (Comment*)>
		<!ATTLIST Check Name ID #REQUIRED>
		<!ATTLIST Check Info CDATA #IMPLIED>
	<!ELEMENT Act (Comment*)>
		<!ATTLIST Act Name ID #REQUIRED>
		<!ATTLIST Act Info CDATA #IMPLIED>
	<!ELEMENT Push (Comment*)>
		<!ATTLIST Push Name ID #REQUIRED>
		<!ATTLIST Push Info CDATA #IMPLIED>
	<!ELEMENT Device (Comment*)>
		<!ATTLIST Device Name ID #REQUIRED>
		<!ATTLIST Device Type CDATA #REQUIRED>
		<!ATTLIST Device Info CDATA #IMPLIED>
	<!ELEMENT Comment (#PCDATA)>
		<!ATTLIST Comment Author CDATA #REQUIRED>
]>
";
		}//function
	}//class
}//ns

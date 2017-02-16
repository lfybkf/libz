#pragma warning disable 1591

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
		private List<State> states = new List<State>();
		private List<Transition> transitions = new List<Transition>();
		private List<Guard> guards = new List<Guard>();
		private List<Push> pushes = new List<Push>();

		public IEnumerable<State> States { get { return states; } }
		public IEnumerable<Transition> Transitions { get { return transitions; } }
		public IEnumerable<Guard> Guards { get { return guards; } }
		public IEnumerable<Push> Pushes { get { return pushes; } }

		public State getState(string name) {return States.FirstOrDefault(z => z.Name == name);}
		public Guard getGuard(string name) { return Guards.FirstOrDefault(z => z.Name == name); }
		public Push getPush(string name) { return Pushes.FirstOrDefault(z => z.Name == name); }

		internal override void Read(XElement src)
		{
			base.Read(src);

			FillListFromXlist<State>(states, src.Elements(R.State));
			FillListFromXlist<Transition>(transitions, src.Elements(R.Transition));
			FillListFromXlist<Guard>(guards, src.Elements(R.Guard));
			FillListFromXlist<Push>(pushes, src.Elements(R.Push));

			States.forEach(z => z.MachineName = Name);
			Transitions.forEach(z => z.MachineName = Name);
			Guards.forEach(z => z.MachineName = Name);
			Pushes.forEach(z => z.MachineName = Name);
		}//function

		internal bool HasState(string name) { return States.Any(z => z.Name == name); }
		internal bool HasGuard(string name) { return Guards.Any(z => z.Name == name); }
		internal bool HasPush(string name) { return Pushes.Any(z => z.Name == name); }

		public override bool Validate()
		{
			Action<AttrzME> validate = (z) =>
				{
					z.Validate();
					addErrors(z.errors);
					addWarnings(z.warnings);
				};

			States.forEach(validate);
			Transitions.forEach(validate);
			Guards.forEach(validate);
			Pushes.forEach(validate);

			return hasErrors;
		}//function

		public static string zXmlDTD()
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

#pragma warning restore 1591
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
		public string Name;
		public string Info;
		private List<State> _states = new List<State>();
		private List<Transition> _transitions = new List<Transition>();
		private List<Act> _acts = new List<Act>();
		private List<Check> _checks = new List<Check>();
		private List<Device> _devices = new List<Device>();

		public IEnumerable<State> states { get { return _states; } }
		public IEnumerable<Transition> transitions { get { return _transitions; } }
		public IEnumerable<Act> acts { get { return _acts; } }
		public IEnumerable<Check> checks { get { return _checks; } }
		public IEnumerable<Device> devices { get { return _devices; } }

		internal override void Read(XElement src)
		{
			Name = Get(R.NAME);
			Info = Get(R.INFO, string.Empty);

			FillListFromXlist<State>(_states, src.Elements(R.STATE));
			FillListFromXlist<Transition>(_transitions, src.Elements(R.TRANSITION));
			FillListFromXlist<Act>(_acts, src.Elements(R.ACT));
			FillListFromXlist<Check>(_checks, src.Elements(R.CHECK));
			FillListFromXlist<Device>(_devices, src.Elements(R.DEVICE));
		}//function

		internal bool HasState(string name) { return name != null && states.Any(z => z.Name == name); }
		internal bool HasCheck(string name) { return name != null && checks.Any(z => z.Name == name); }
		internal bool HasAct(string name) { return name != null && acts.Any(z => z.Name == name); }

		internal IEnumerable<string> Validate()
		{
			IEnumerable<string> errors = null;
			string fmtError;

			#region inner errors
			errors = transitions.SelectMany(tr => tr.ValidateInner()).ToArray();
			if (errors.Any()) { return errors; }
			#endregion

			#region complex errors
			Lazy<List<string>> result = new Lazy<List<string>>();
			Action fix_errors = () => { result.Value.AddRange(errors.ToArray()); };

			fmtError = "State={0} is never used";
			errors = states.Where(st => transitions.All(tr => tr.IsStateUsed(st.Name) == false)).Select(st => fmtError.fmt(st.Name)) ;
			fix_errors();

			fmtError = "State {0} contains wrong Enter Act {1}";
			errors = states.Where(st => st.IsEnterGood()).Select(st => fmtError.fmt(st.Name, st.Enter));
			fix_errors();

			fmtError = "State {0} contains wrong Exit Act {1}";
			errors = states.Where(st => st.IsExitGood()).Select(st => fmtError.fmt(st.Name, st.Exit));
			fix_errors();

			fmtError = "Transition={0} contains wrong state From={1}";
			errors = transitions.Where(tr => HasState(tr.From) == false).Select(tr => fmtError.fmt(tr.Name, tr.From));
			fix_errors();

			fmtError = "Transition={0} contains wrong state To={1}";
			errors = transitions.Where(tr => HasState(tr.To) == false).Select(tr => fmtError.fmt(tr.Name, tr.To));
			fix_errors();

			fmtError = "Transition {0} contains wrong Check {1}";
			IEnumerable<string> transition_check_errors = transitions
				.SelectMany(t => t.checks.Select(s => new { tr = t.Name, ch = s }))
				.Where(a => HasCheck(a.ch) == false)
				.Select(a => fmtError.fmt(a.tr, a.ch));
			fix_errors();

			fmtError = "Transition {0} contains wrong Act {1}";
			IEnumerable<string> transition_act_errors = transitions
				.SelectMany(t => t.acts.Select(s => new { tr = t.Name, act = s }))
				.Where(a => HasAct(a.act) == false)
				.Select(a => fmtError.fmt(a.tr, a.act));
			fix_errors();


			#endregion

			if (result.IsValueCreated) { return result.Value; } else { return EmptyStrings; }
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

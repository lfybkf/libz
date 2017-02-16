#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDB.Templating
{
	public class Transition : AttrzME
	{
		private static string fmtName = "({0} -> {1})";
		public override string Name
		{
			get	{	return (From != null && To != null) ? fmtName.fmt(From, To) : "Bad transition";	}
			set	{	;	}
		}
		public string From { get; set; }
		public string To { get; set; }
		public IEnumerable<State> States { get { yield return machine.getState(From); yield return machine.getState(To); } }

		string[] guards = R.EmptyStrings;
		string[] pushes = R.EmptyStrings;
		public IEnumerable<Guard> Guards { get { return guards.Select(s => machine.getGuard(s)); } }
		public IEnumerable<Push> Pushes { get { return pushes.Select(s => machine.getPush(s)); } }


		internal override void Read(System.Xml.Linq.XElement src)
		{
			base.Read(src);
			From = Get(R.From);
			To = Get(R.To);
			guards = Gef(R.Guards, string.Empty).Split(' ').Where(s => s.Length > 0).ToArray();
			pushes = Gef(R.Pushes, string.Empty).Split(' ').Where(s => s.Length > 0).ToArray();
		}//function

		public override bool Validate()
		{
			if (From == null) { addError("Transition={0} has no From".fmt(Name)); }
			if (To == null) { addError("Transition={0} has no To".fmt(Name)); }
			if (pushes.Any() == false) { addError("Transition={0} has no Pushes".fmt(Name)); }
			if (machine.HasState(From) == false)	{addError("Transition={0} has wrong From".fmt(Name));}//if
			if (machine.HasState(To) == false)		{addError("Transition={0} has wrong To".fmt(Name));}//if
			pushes.forEach(s => { if (!machine.HasPush(s)) { addError("Transition={0} has wrong Push={1}".fmt(Name, s)); } });
			guards.forEach(s => { if (!machine.HasGuard(s)) { addError("Transition={0} has wrong Guard={1}".fmt(Name, s)); } });
			return hasErrors;
		}//function

		public bool IsStateUsed(string name) {return States.Any(st => st.Name == name);} 
		public bool IsGuardUsed(string name) { return guards.Contains(name); }
		public bool IsPushUsed(string name) { return pushes.Contains(name); }
		public bool HasGuards { get { return Guards.Any(); } }
	}//class
}//ns

#pragma warning restore 1591
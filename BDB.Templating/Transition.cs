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
		public string From;
		public string To;

		private IEnumerable<string> _checks, _acts, _pushes;
		public IEnumerable<string> checks { get { return _checks; } }
		public IEnumerable<string> acts { get { return _acts; } }
		public IEnumerable<string> pushes { get { return _pushes; } }
		public IEnumerable<string> states { get { yield return From; yield return To; } }

		internal override void Read(System.Xml.Linq.XElement src)
		{
			base.Read(src);
			From = Get(R.FROM);
			To = Get(R.TO);
			_checks = Get(R.Checks, string.Empty).Split(' ').Where(s => s.Length > 0).ToArray();
			_acts = Get(R.Acts, string.Empty).Split(' ').Where(s => s.Length > 0).ToArray();
			_pushes = Get(R.Pushes, string.Empty).Split(' ').Where(s => s.Length > 0).ToArray();
		}//function

		public override bool Validate()
		{
			if (From == null) { addError("Transition={0} has no From".fmt(Name)); }
			if (To == null) { addError("Transition={0} has no To".fmt(Name)); }
			if (pushes.Any() == false) { addError("Transition={0} has no Pushes".fmt(Name)); }
			if (machine.HasState(From) == false)	{addError("Transition={0} has wrong From".fmt(Name));}//if
			if (machine.HasState(To) == false)		{addError("Transition={0} has wrong To".fmt(Name));}//if
			pushes.forEach(s => { if (!machine.HasPush(s)) { addError("Transition={0} has wrong Push={1}".fmt(Name, s)); } });
			checks.forEach(s => { if (!machine.HasCheck(s)) { addError("Transition={0} has wrong Check={1}".fmt(Name, s)); } });
			acts.forEach(s => { if (!machine.HasAct(s)) { addError("Transition={0} has wrong Act={1}".fmt(Name, s)); } });
			return hasErrors;
		}//function

		public bool IsStateUsed(string name)	{return states.Contains(name);} 
		public bool IsCheckUsed(string name) { return checks.Contains(name); }
		public bool IsActUsed(string name) { return acts.Contains(name); }
		public bool IsPushUsed(string name) { return pushes.Contains(name); }
		public bool HasChecks { get { return checks.Any(); } }
		public bool HasActs { get { return acts.Any(); } }
	}//class
}//ns

#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BDB.Templating
{
	public class State: AttrzME
	{
		public string Enter { get; set; }
		public string Exit { get; set; }

		string[] enterGuards = R.EmptyStrings;
		string[] exitGuards = R.EmptyStrings;
		public IEnumerable<Guard> EnterGuards { get { return enterGuards.Select(s => machine.getGuard(s)); } }
		public IEnumerable<Guard> ExitGuards { get { return exitGuards.Select(s => machine.getGuard(s)); } }

		internal override void Read(XElement src)
		{
			base.Read(src);
			Enter = Get(R.Enter);
			Exit = Get(R.Exit);
			if (Enter.isEmpty() == false) { enterGuards = Enter.Split(' '); }
			if (Exit.isEmpty() == false) { exitGuards = Exit.Split(' '); }
		}//function

		public override bool Validate()
		{
			if (machine.Transitions.Any(z => z.IsStateUsed(Name)) == false)
			{
				addWarning("State={0} is not used".fmt(Name));
			}//if
			return hasErrors;
		}

		public bool IsGuardUsed(string name) { return enterGuards.Contains(name) || exitGuards.Contains(name); }
	}//class
}//ns

#pragma warning restore 1591
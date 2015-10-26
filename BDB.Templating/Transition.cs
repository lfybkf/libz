using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDB.Templating
{
	public class Transition : AttrzME
	{
		public string From;
		public string To;

		private IEnumerable<string> _checks, _acts;
		public IEnumerable<string> checks { get { return _checks; } }
		public IEnumerable<string> acts { get { return _acts; } }
		internal override void Read(System.Xml.Linq.XElement src)
		{
			base.Read(src);
			From = Get(R.FROM);
			To = Get(R.TO);
			_checks = Get(R.Checks, string.Empty).Split(' ').ToArray();
			_acts = Get(R.Acts, string.Empty).Split(' ').ToArray();
		}//function

		public override bool Validate()
		{
			if (From == null) { addError("Transition={0} has no From".fmt(Name)); }
			if (To == null) { addError("Transition={0} has no To".fmt(Name)); }
			if (machine.HasState(From) == false)
			{
				addError("Transition={0} has wrong From".fmt(Name));
			}//if
			return hasErrors;
		}//function

		public bool IsStateUsed(string name)	{return From == name || To == name;} 
		public bool IsCheckUsed(string name) { return checks.Contains(name); }
		public bool IsActUsed(string name) { return acts.Contains(name); }
	}//class
}//ns

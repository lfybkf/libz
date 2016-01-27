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
		internal override void Read(XElement src)
		{
			base.Read(src);
			Enter = Get(R.ENTER);
			Exit = Get(R.EXIT);
		}//function

		public override bool Validate()
		{
			if (Enter != null && machine.HasAct(Enter) == false)
			{
				addError("State={0} has wrong Enter".fmt(Name));
			}//if

			if (Exit != null && machine.HasAct(Exit) == false)
			{
				addError("State={0} has wrong Exit".fmt(Name));
			}//if

			if (machine.transitions.Any(z => z.IsStateUsed(Name)) == false)
			{
				addWarning("State={0} is not used".fmt(Name));
			}//if
			return hasErrors;
		}
		public bool IsActUsed(string name) { return Enter == name || Exit == name; }
	}//class
}//ns

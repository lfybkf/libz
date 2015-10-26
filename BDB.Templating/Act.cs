using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDB.Templating
{
	public class Act:AttrzME
	{
		public override bool Validate()
		{
			bool UsedInStates = machine.states.Any(z => z.IsActUsed(Name));
			bool UsedInTransitions = machine.transitions.Any(z => z.IsActUsed(Name));
			if (!UsedInStates && !UsedInTransitions )
			{
				addWarning("Act={0} is never used".fmt(Name));
			}//if
			
			return hasErrors;
		}//function
	}//class
}//ns

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDB.Templating
{
	///<summary>guard</summary> 
	public class Guard:AttrzME
	{
		///<summary>validate</summary> 
		public override bool Validate()
		{
			bool UsedInStates = machine.States.Any(z => z.IsGuardUsed(Name));
			bool UsedInTransitions = machine.Transitions.Any(z => z.IsGuardUsed(Name));
			if (!UsedInStates && !UsedInTransitions )
			{
				addWarning("Guard={0} is never used".fmt(Name));
			}//if
			return hasErrors;
		}//function
	}//class
}//ns

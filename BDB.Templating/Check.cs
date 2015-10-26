using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDB.Templating
{
	public class Check:AttrzME
	{
		public override bool Validate()
		{
			bool UsedInTransitions = machine.transitions.Any(z => z.IsCheckUsed(Name));
			if (UsedInTransitions == false)
			{
				addWarning("Check={0} is not used".fmt(Name));	
			}//if
			return hasErrors;
		}
	}//class
}//ns

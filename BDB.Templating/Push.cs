using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB.Templating
{
	public class Push: AttrzME
	{
		public override bool Validate()
		{
			if (machine.transitions.Any(tr => tr.IsPushUsed(Name)) == false)
			{
				addWarning("Push={0} is never used".fmt(Name));
			}//if 
			return hasErrors;
		}
	}//class
}//ns

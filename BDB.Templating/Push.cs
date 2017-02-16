#pragma warning disable 1591

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
			if (machine.Transitions.Any(tr => tr.IsPushUsed(Name)) == false)
			{
				addWarning("Push={0} is never used".fmt(Name));
			}//if 
			return hasErrors;
		}//function

		public IEnumerable<Transition> transitions
		{
			get { return machine.Transitions.Where(z => z.IsPushUsed(Name)); }
		}//property
		
	}//class
}//ns

#pragma warning restore 1591
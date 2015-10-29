using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDB.Templating
{
	public class Device:AttrzME
	{
		public string Type;
		public string Getter;

		internal override void Read(System.Xml.Linq.XElement src)
		{
			base.Read(src);
			Type = Get(R.TYPE);
			Getter = Get(R.GETTER);
		}

		public override bool Validate()
		{
			bool UsedInChecks = machine.checks.Any(z => z.IsDeviceUsed(Name));
			bool UsedInActs = machine.acts.Any(z => z.IsDeviceUsed(Name));
			if (!UsedInActs && !UsedInChecks)
			{
				addWarning("Device={0} is never used".fmt(Name));
			}//if
			return hasErrors;
		}
	}//class
}//ns

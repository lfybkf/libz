using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDB.Templating
{
	public class Check:AttrzME
	{
		public string Device { get; set; }
		public string Test { get; set; }

		internal override void Read(System.Xml.Linq.XElement src)
		{
			base.Read(src);
			Device = Get(R.DEVICE);
			Test = Get(R.TEST);
		}

		public override bool Validate()
		{
			if (machine.transitions.Any(z => z.IsCheckUsed(Name)) == false)
			{
				addWarning("Check={0} is not used".fmt(Name));	
			}//if
			if (Device != null && machine.HasDevice(Device) == false)
			{
				addError("Check={0} has wrong Device={1}".fmt(Name, Device));
			}//if
			return hasErrors;
		}

		internal bool IsDeviceUsed(string Name)
		{
			return Name == Device;
		}
	}//class
}//ns

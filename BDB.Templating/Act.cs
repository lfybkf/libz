﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDB.Templating
{
	public class Act:AttrzME
	{
		public string Device;
		public string Change;

		internal override void Read(System.Xml.Linq.XElement src)
		{
			base.Read(src);
			Device = Get(R.DEVICE);
			Change = Get(R.CHANGE);
		}

		public override bool Validate()
		{
			bool UsedInStates = machine.states.Any(z => z.IsActUsed(Name));
			bool UsedInTransitions = machine.transitions.Any(z => z.IsActUsed(Name));
			if (!UsedInStates && !UsedInTransitions )
			{
				addWarning("Act={0} is never used".fmt(Name));
			}//if
			if (Device != null && machine.HasDevice(Device) == false)
			{
				addError("Act={0} has wrong Device={1}".fmt(Name, Device));
			}//if
			return hasErrors;
		}//function

		internal bool IsDeviceUsed(string Name)
		{
			return Name == Device;
		}
	}//class
}//ns
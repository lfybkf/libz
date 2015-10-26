﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDB.Templating
{
	public class Device:AttrzME
	{
		public string Type;
		internal override void Read(System.Xml.Linq.XElement src)
		{
			base.Read(src);
			Type = Get(R.TYPE);
		}
	}//class
}//ns

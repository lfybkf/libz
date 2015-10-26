using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDB.Templating
{
	public class Check:Attrz
	{
		public string Name;
		public string Info;
		internal override void Read(System.Xml.Linq.XElement src)
		{
			Name = Get(R.NAME);
			Info = Get(R.INFO, string.Empty);
		}
	}
}

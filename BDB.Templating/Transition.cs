using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDB.Templating
{
	public class Transition : Attrz
	{
		public string Name;
		public string From;
		public string To;
		public string Info;
		public IEnumerable<string> checks;
		public IEnumerable<string> acts;
		public override void Read(System.Xml.Linq.XElement src)
		{
			Name = Get(R.NAME);
			Info = Get(R.INFO, string.Empty);
		}
	}
}

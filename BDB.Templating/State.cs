using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BDB.Templating
{
	public class State: Attrz
	{
		public string Name;
		public string Info;
		public override void Read(XElement src)
		{
			Name = Get(R.NAME);
			Info = Get(R.INFO, string.Empty);
		}//function
	}//class
}//ns

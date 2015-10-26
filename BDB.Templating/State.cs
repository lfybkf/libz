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
		internal Machine machine;
		public string Name;
		public string Enter;
		public string Exit;
		public string Info;
		internal override void Read(XElement src)
		{
			Name = Get(R.NAME);
			Enter = Get(R.ENTER);
			Exit = Get(R.EXIT);
			Info = Get(R.INFO, string.Empty);
		}//function

		public bool IsActUsed(string name) { return Enter == name || Exit == name; }
		public bool IsEnterGood() { return Enter == null || machine.HasAct(Enter); }
		public bool IsExitGood() { return Exit == null || machine.HasAct(Exit); }
	}//class
}//ns

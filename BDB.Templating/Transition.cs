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
		private IEnumerable<string> _checks, _acts;
		public IEnumerable<string> checks { get { return _checks; } }
		public IEnumerable<string> acts { get { return _acts; } }
		internal override void Read(System.Xml.Linq.XElement src)
		{
			Name = Get(R.NAME);
			Info = Get(R.INFO, string.Empty);
			From = Get(R.FROM);
			To = Get(R.TO);
			_checks = Get(R.Checks, string.Empty).Split(' ').ToArray();
			_acts = Get(R.Acts, string.Empty).Split(' ').ToArray();
		}//function

		private static string fmtValidate = "Transition {0} has error - {1}";
		internal override IEnumerable<string> ValidateInner()
		{
			Lazy<List<string>> result = new Lazy<List<string>>();
			if (From == null) { result.Value.Add(fmtValidate.fmt(Name, "no state From")); }
			if (To == null) { result.Value.Add(fmtValidate.fmt(Name, "no state To")); }
			if (result.IsValueCreated) { return result.Value; } else { return EmptyStrings; }
		}//function

		public bool IsStateUsed(string name)	{return From == name || To == name;} 
		public bool IsCheckUsed(string name) { return checks.Contains(name); }
		public bool IsActUsed(string name) { return acts.Contains(name); }
	}//class
}//ns

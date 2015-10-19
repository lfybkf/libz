using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BDB.Templating
{
	public abstract class Attrz
	{
		private Dictionary<string, string> attrS = new Dictionary<string, string>();
		public bool Has(string key) { return attrS.ContainsKey(key); }
		public bool Has(string key, string value) { return attrS.get(key) == value; }
		public bool HasNot(string key) { return !attrS.ContainsKey(key); }
		public bool HasNot(string key, string value) { return attrS.get(key) != value; }
		public string Get(string key) { return attrS.get(key); }
		public string Get(string key, string defaultValue) { return attrS.get(key, defaultValue); }

		public void Fill(IEnumerable<XAttribute> source) { foreach (XAttribute attr in source) { attrS.Add(attr.Name.LocalName, attr.Value); }}
	}
}

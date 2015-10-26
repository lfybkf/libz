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
		//private string InnerContent;
		private Dictionary<string, string> attrS = new Dictionary<string, string>();
		public bool Has(string key) { return attrS.ContainsKey(key); }
		public bool Has(string key, string value) { return attrS.get(key) == value; }
		public bool HasNot(string key) { return !attrS.ContainsKey(key); }
		public bool HasNot(string key, string value) { return attrS.get(key) != value; }
		public string Get(string key) { return attrS.get(key); }
		public string Get(string key, string defaultValue) { return attrS.get(key, defaultValue); }

		internal abstract void Read(XElement src);

		public static string[] EmptyStrings = {};
		internal virtual IEnumerable<String> ValidateInner() { return EmptyStrings;}
		
		internal void FillContentFromAttrs(XElement src)
		{
			if (src.IsEmpty) { attrS.Add(string.Empty, src.Value); }//if
			foreach (XAttribute attr in src.Attributes())
			{
				attrS.Add(attr.Name.LocalName, attr.Value); 
			}//for
		}//function

		internal static void FillListFromXlist<T>
			(IList<T> items, IEnumerable<XElement> xtags)
			where T : Attrz, new()
		{
			T item = null;
			foreach (var tag in xtags)
			{
				item = new T();
				item.FillContentFromAttrs(tag);
				item.Read(tag);
				items.Add(item);
			}//for
		}//function

	}//class
}//ns

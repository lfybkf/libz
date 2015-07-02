using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BDB.Templating
{
	public class Map
	{
		List<Obj> _objects = new List<Obj>();
		public IEnumerable<Obj> objects { get { return _objects; } }

		public string Template { get; set; }

		public void Load()
		{
			string dir = Path.GetDirectoryName(Template);
			XDocument xdoc = XDocument.Load(Path.Combine(dir, "Model.xml"));

			Obj item = null;
			foreach (var xObj in xdoc.Root.Elements(R.OBJECT))
			{
				item = new Obj();
				item.Read(xObj);
				_objects.Add(item);
			}//for
		}//function
	}//class
}//ns

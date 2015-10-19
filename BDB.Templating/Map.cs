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

		public string ModelDirectory = Environment.CurrentDirectory;
		public string ModelPattern = "Model*.xml";

		public void Load()
		{
			var ModelFiles = Directory.EnumerateFiles(ModelDirectory, ModelPattern);
			XDocument xdoc = null;
			Obj item = null;

			foreach (var file in ModelFiles)
			{
				xdoc = XDocument.Load(file);
				foreach (var xObj in xdoc.Root.Elements(R.OBJECT))
				{
					item = new Obj();
					item.Read(xObj);
					_objects.Add(item);
				}//for
			}//for
		}//function
	}//class
}//ns

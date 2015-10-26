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
		public static Map Current = null;
		List<Obj> _objects = new List<Obj>();
		public IEnumerable<Obj> objects { get { return _objects; } }
		List<Machine> _machines = new List<Machine>();
		public IEnumerable<Machine> machines { get { return _machines; } }

		public string ModelDirectory = Environment.CurrentDirectory;
		public string ModelPattern = "Model*.xml";
		public string MachineDirectory = Environment.CurrentDirectory;
		public string MachinePattern = "Machine*.xml";

		public Map()
		{
			Current = this;
		}//constructor

		public void Load()
		{
			var files = Directory.EnumerateFiles(ModelDirectory, ModelPattern);
			XDocument xdoc = null;

			foreach (var file in files)
			{
				xdoc = XDocument.Load(file);
				Attrz.FillListFromXlist<Obj>(_objects, xdoc.Root.Elements(R.OBJECT));
				_objects.forEach(o => o.FileSource = Path.GetFileNameWithoutExtension(file));
			}//for
		}//function

		public void LoadMachines()
		{
			var files = Directory.EnumerateFiles(MachineDirectory, MachinePattern);
			XDocument xdoc = null;

			foreach (var file in files)
			{
				xdoc = XDocument.Load(file);
				Attrz.FillListFromXlist<Machine>(_machines, xdoc.Root.Elements(R.MACHINE));
			}//for
		}//function

	}//class
}//ns

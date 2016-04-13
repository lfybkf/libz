using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB.Web
{
	public class Route
	{
		public Type Controller { get; private set; }
		public string Name { get; private set; }

		public Route(string name, Type controller)
		{
			this.Name = name;
			this.Controller = controller;
		}
	}
}

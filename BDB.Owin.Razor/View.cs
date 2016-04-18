using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB.Owin.Razor
{
	public class View
	{
		public string ContentType = CONTENT_TYPE.HTML;
		public string Name { get; set; }
		public object Model { get; set; }
	}//class
}//namespace

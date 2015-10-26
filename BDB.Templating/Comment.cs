using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB.Templating
{
	public class Comment: Attrz
	{
		public string Author;
		public string Content;

		internal override void Read(System.Xml.Linq.XElement src)
		{
			Author = Get(R.AUTHOR);
			Content = Get(string.Empty);
		}//function
	}//class
}//ns

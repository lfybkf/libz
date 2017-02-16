using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB.Templating
{
	///<summary>коммент</summary> 
	public class Comment: Attrz
	{
		///<summary>автор</summary> 
		public string Author { get; set; }
		///<summary>содержание</summary> 
		public string Content { get; set; }

		internal override void Read(System.Xml.Linq.XElement src)
		{
			Author = Get(R.Author);
			Content = Get(string.Empty);
		}//function
	}//class
}//ns

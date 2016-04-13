﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB.Web
{
	public interface IView
	{
		object Model { get; set; }
		string ViewName { get; set; }
	}//interface

	public class View : IView
	{
		public object Model { get; set; }
		public string ViewName { get; set; }

		public View(string viewName, object model)
		{
			this.ViewName = viewName;
			this.Model = model;
		}
	}//class
}

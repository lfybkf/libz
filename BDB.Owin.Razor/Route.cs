using System;
using Microsoft.Owin;

namespace BDB.Owin.Razor
{
	public class Route
	{
		public Route(string Name, Func<IOwinContext, View> getView)
		{
			this.Name = Name;
			this.getView = getView;
		}//constructor

		public string Name { get; private set; }
		public Func<IOwinContext, View> getView { get; private set; }
	}//class

	
}//namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB.Web
{
	public static class OWIN
	{
		public const string RequestMethod = "owin.RequestMethod";
		public const string RequestPath = "owin.RequestPath";
		public const string ResponseBody = "owin.ResponseBody";
		public const string ResponseHeaders = "owin.ResponseHeaders";
	}

	public static	class VIEW
	{
		
		public const string all = "all";
		public const string index = "index";
		public const string view = "view";
	}//class
}

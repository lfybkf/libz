using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace BDB.Owin.Razor
{
	public class MiddlePathFilter : OwinMiddleware
	{
		static List<string> forbidden = new List<string>();
		public static void AddForbidden(string s)
		{
			forbidden.Add(s);
		}//function

		public MiddlePathFilter(OwinMiddleware next) : base(next) { }

		public async override Task Invoke(IOwinContext context)
		{
			string path = context.Request.Path.Value;
			string sForbidden = forbidden.FirstOrDefault(s => path.Contains(s));
			if (sForbidden != null)
			{
				//Console.WriteLine("forbidden {0} in {1}", sForbidden, path);
				await Task.FromResult<object>(null);
			}//if
			else
			{
				await Next.Invoke(context);
			}//else
		}//function
	}//class
}//namespace

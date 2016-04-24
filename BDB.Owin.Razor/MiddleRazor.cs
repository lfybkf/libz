using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;
using io = System.IO;
using text = System.Text;
using BDB;


namespace BDB.Owin.Razor
{
	public class MiddleRazor : OwinMiddleware
	{
		public string LastError
		{
			get
			{
				return ctx.Environment.get(Mark) as string;
			}
			set
			{
				ctx.Environment[Mark] = value;
			}
		}

		public const string Mark = "MiddleRazor";
		public static text.Encoding encoding = text.Encoding.Default;
		public static Func<string, string> getRouteFromUri;
		private IOwinContext ctx;

		private static List<Route> routes = new List<Route>();
		public static void AddRoute(Route route)
		{
			if (routes.Any(z => z.Name == route.Name))
			{
				throw new Exception("Not unique route with Name=" + route.Name);
			}//if
			routes.Add(route);
		}//function

		public MiddleRazor(OwinMiddleware next) : base(next) { }

		public async override Task Invoke(IOwinContext context)
		{
			ctx = context;
			if (ctx.Request.Method == WEB.GET)
			{
				doGet();
				if (LastError.isEmpty() == false)
				{
					Console.WriteLine(LastError);	
				}//if
			}//if
			await Next.Invoke(context);
		}

		private async void doGet()
		{
			if (ctx.Request.Path.HasValue==false) { LastError = "path has no value"; return; }
			string uri = ctx.Request.Path.Value;
			string routeName = getRouteFromUri(uri);
			if (routeName.isEmpty()) { LastError = "cant get route from uri"; return; }
			var route = routes.FirstOrDefault(z => z.Name == routeName);
			if (route == null) { LastError = routeName + " is not here"; return; }
			View view = route.getView(ctx); 
			if (view == null) { LastError = LastError ?? "view is null"; return; }
			if (view.ContentType == CONTENT_TYPE.Url)
			{
				var redirectTo = view.Model.ToString();
				Console.WriteLine("redirected to {0}".fmt(redirectTo));
				ctx.Response.Redirect(redirectTo);
				return;
			}//if

			ctx.Response.Headers[CONTENT_TYPE.Header] = view.ContentType;
			string content = view.Parse();
			//Console.WriteLine("{0} is parsed", view.Name);
			if (content.isEmpty()) { LastError = LastError ?? "view is not parsed "; return; }
			await ctx.Response.Body.WriteAsync(encoding.GetBytes(content), 0, content.Length);
		}
	}//class
}//namespace


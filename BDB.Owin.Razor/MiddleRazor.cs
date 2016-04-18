using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;
using io = System.IO;
using text = System.Text;
using json = System.Web.Script.Serialization;
using RazorEngine;
using RazorEngine.Templating;
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

		public delegate View getViewFromUri(IOwinContext context);
		public const string Mark = "MiddleRazor";
		public static string TemplatePathBase = Environment.CurrentDirectory.addToPath("View");
		public static text.Encoding encoding = text.Encoding.Default;
		public static Func<string, string> getRouteFromUri;

		private const string Ext = ".cshtml";
		private IOwinContext ctx;

		private static IDictionary<string, getViewFromUri> routes = new Dictionary<string, getViewFromUri>();
		public static void AddRoute(string routeName, getViewFromUri func)
		{
			routes[routeName] = func;
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
			if (routes.ContainsKey(routeName) == false) { LastError = routeName + " is not here"; return; }
			getViewFromUri getView = routes[routeName];
			if (getView == null) { LastError = "no function view"; return; }
			View view = getView(ctx);
			if (view == null) { LastError = LastError ?? "view is null"; return; }

			ctx.Response.Headers[CONTENT_TYPE.Header] = view.ContentType;
			string content = string.Empty;
			if (view.ContentType == CONTENT_TYPE.HTML)
			{
				string templatePath = TemplatePathBase.addToPath(view.Name) + Ext;
				if (io.File.Exists(templatePath) == false) { LastError = templatePath + " no exists"; return; }
				string template = io.File.ReadAllText(templatePath);
				var razor = RazorEngine.Engine.Razor;
				content = razor.RunCompile(template, view.Name, null, view.Model);
			}//if
			else if (view.ContentType == CONTENT_TYPE.JSON)
			{
				var serializer = new json.JavaScriptSerializer();
				content = serializer.Serialize(view.Model);
			}//if
			else
			{
				content = view.Model.ToString();
			}//else
			await ctx.Response.Body.WriteAsync(encoding.GetBytes(content), 0, content.Length);
		}
	}//class
}//namespace


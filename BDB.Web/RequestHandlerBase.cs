using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using io = System.IO;
using RazorEngine;
using RazorEngine.Templating;

namespace BDB.Web
{
	public abstract class RequestHandlerBase
	{
		protected IDictionary<string, object> Environment { get; private set; }
		protected IEnumerable<Route> Routes { get; private set; }

		protected string RequestPath
		{
			get { return (string)this.Environment[OWIN.RequestPath]; }
			set { this.Environment[OWIN.RequestPath] = value; }
		}

		public RequestHandlerBase(IDictionary<string, object> env, IEnumerable<Route> routes)
		{
			this.Environment = env;
			this.Routes = routes;
			this.InitResponseType();
		}

		private void InitResponseType()
		{
			var header = (IDictionary<string, string[]>)this.Environment[OWIN.ResponseHeaders];
			header.Add("Content-Type", new[] { "text/html" });
		}

		public abstract Task<object> Handle();

		protected string GetViewPath(string controller, string viewName)
		{
			return io.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "View", controller, viewName + ".cshtml");
		}

		protected async Task WriteResponse(string viewPath, object model)
		{
			if (!io.File.Exists(viewPath)) { throw new Exception("View not found. Path: " + viewPath); }

			using (var writer = new io.StreamWriter((io.Stream)this.Environment[OWIN.ResponseBody]))
			{
				var template = new io.StreamReader(viewPath).ReadToEnd();
				var razor = RazorEngine.Engine.Razor;
				var result = razor.RunCompile(template, null, model);
				await writer.WriteAsync(result);
			}
		}

		protected Route GetRoute(string routeName)
		{
			var route = this.Routes.FirstOrDefault(x => x.Name.ToLower() == routeName.ToLower());
			if (route == null)
				throw new Exception("Route not found: " + routeName);

			return route;
		}

		protected IView InvokeController(Type controller, string actionName)
		{
			var controllerInstance = Activator.CreateInstance(controller, false);
			var actionMethod = controller.GetMethod(actionName);

			if (actionMethod == null)
				throw new Exception("Action not found: " + actionName);

			return (IView)actionMethod.Invoke(controllerInstance, new object[] { });
		}

		protected string[] GetControllerAndAction()
		{
			var result = new string[2];
			var requestPath = this.RequestPath.Substring(1).Split('/');

			result[0] = requestPath[0];
			result[1] = (requestPath.Length > 1) ? requestPath[1] : "Index";

			return result;
		}
	}

}

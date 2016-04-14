using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB.Web
{
	class RequestHandlerGET : RequestHandlerBase
	{
		public RequestHandlerGET(IDictionary<string, object> env, IEnumerable<Route> routes) : base(env, routes) { }

		public async override Task<object> Handle()
		{
			var cai = base.parsePath();
			var route = base.getRoute(cai.Controller);
			var view = base.InvokeController(route.Controller, cai.Action);
			var viewPath = base.GetViewPath(cai.Controller, view.ViewName);
			await base.WriteResponse(viewPath, view.Model);

			return Task.FromResult<object>(null);
		}
	}//class
}

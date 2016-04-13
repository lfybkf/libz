using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB.Web
{
	public static class RequestFactory
	{
		const string GET = "GET";
		const string POST = "POST";

		public static RequestHandlerBase Get(IDictionary<string, object> env, IEnumerable<Route> routes)
		{
			var httpMethod = ((string)env[OWIN.RequestMethod]).ToUpper();

			switch (httpMethod)
			{
				case GET: return new RequestHandlerGET(env, routes);
				case POST: throw new NotImplementedException("POST handler");
				default: throw new NotImplementedException("No handler found for: " + httpMethod);
			}
		}
	}
}

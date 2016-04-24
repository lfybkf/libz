using System;
using System.Collections.Generic;
using System.Linq;
using io = System.IO;
using text = System.Text;
using json = System.Web.Script.Serialization;
using RazorEngine.Templating;


namespace BDB.Owin.Razor
{
	public class View
	{
		public static string TemplatePathBase = Environment.CurrentDirectory.addToPath("View");
		private const string Ext = ".cshtml";
		private Lazy<json.JavaScriptSerializer> serializer = new Lazy<json.JavaScriptSerializer>();

		public string ContentType = CONTENT_TYPE.HTML;
		public string Name { get; set; }
		public object Model { get; set; }
		public string LastError { get; set; }

		public string Parse()
		{
			//Console.WriteLine("View is parsing - {0}", Name);
			string result = string.Empty;
			if (ContentType == CONTENT_TYPE.HTML)
			{
				string templatePath = TemplatePathBase.addToPath(Name) + Ext;
				if (io.File.Exists(templatePath) == false) { LastError = templatePath + " no exists"; return string.Empty; }
				string template = io.File.ReadAllText(templatePath);
				var razor = RazorEngine.Engine.Razor;
				result = razor.RunCompile(template, Name, null, Model);
			}//if
			else if (ContentType == CONTENT_TYPE.JSON)
			{
				result = serializer.Value.Serialize(Model);
			}//if
			else
			{
				result = Model.ToString();
			}//else
			return result;
		}//function

		public static View CreateTEXT(object model)
		{
			return new View { ContentType = CONTENT_TYPE.TEXT, Model = model };
		}//function
		public static View CreateJSON(object model)
		{
			return new View { ContentType = CONTENT_TYPE.JSON, Model = model };
		}//function
		public static View CreateUrl(object model)
		{
			return new View { ContentType = CONTENT_TYPE.Url, Model = model };
		}//function
	}//class
}//namespace

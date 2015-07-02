using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace BDB
{
	public class SimpleLogger
	{
		static string CR = "\r\n";
		public SimpleLogger(string FormatName)
		{
			this.FormatName = FormatName;
			pathInfo = Path.Combine(Environment.CurrentDirectory, string.Format(FormatName, "Info"));
			pathError = Path.Combine(Environment.CurrentDirectory, string.Format(FormatName, "Error"));
			try
			{
				File.Delete(pathInfo);
				File.Delete(pathError);
			}//try
			catch (Exception)
			{
				
			}//catch
		}//constructor

		string FormatName;
		string pathInfo;
		string pathError;

		public void Info(object Message)	{	File.AppendAllText(pathInfo, Message.ToString() + CR, Encoding.Unicode);}
		public void Error(object Message) { File.AppendAllText(pathError, Message.ToString() + CR); }

		public void Info(object Message, bool Yes) { if (Yes) { Info(Message); } }
		public void Error(object Message, bool Yes) { if (Yes) { Error(Message); } }
	}//class
}//ns

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace BDB
{
	///<summary>see name</summary>
	public class SimpleLogger
	{
		static string CR = Environment.NewLine;
		
		static SimpleLogger _instance;
		///<summary>see name</summary>
		public static SimpleLogger Instance { get { if (_instance == null) { _instance = new SimpleLogger("static"); } return _instance; } }

		///<summary>see name</summary>
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

		///<summary>see name</summary>
		public void Info(object Message)	{	File.AppendAllText(pathInfo, Message.ToString() + CR, Encoding.Unicode);}
		///<summary>see name</summary>
		public void Error(object Message) { File.AppendAllText(pathError, Message.ToString() + CR); }
		///<summary>see name</summary>
		public void Info(object Message, bool Yes) { if (Yes) { Info(Message); } }
		///<summary>see name</summary>
		public void Error(object Message, bool Yes) { if (Yes) { Error(Message); } }
	}//class
}//ns

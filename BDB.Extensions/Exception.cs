using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;

namespace BDB
{
	///<summary>ExceptionExtension</summary> 
	public static class ExceptionExtension
	{
		/// <summary>
		/// Win32 Error Codes
		/// </summary>
		public enum Win32EC
		{
			///<summary>see name of enum</summary>
			ERROR_INVALID_FUNCTION = 1,
			///<summary>see name of enum</summary>
			ERROR_FILE_NOT_FOUND = 2,
			///<summary>see name of enum</summary>
			ERROR_PATH_NOT_FOUND = 3,
			///<summary>see name of enum</summary>
			ERROR_TOO_MANY_OPEN_FILES = 4,
			///<summary>see name of enum</summary>
			ERROR_ACCESS_DENIED = 5
		}//enum
		
		///<summary>see name</summary>
		public static List<string> OtherMessages(this Exception E)
		{
			List<string> Ret = new List<string>();
			Ret.Add(E.Message);
			if (E is SqlException)
			{
				SqlException hE = (SqlException)E;
				Ret.AddRange(hE.Errors.Cast<SqlError>()
					.Select(e => e.Message));
			}//if
			else if (E is TargetInvocationException)
			{
				TargetInvocationException hE = (TargetInvocationException)E;
				Ret.Add(hE.InnerException.Message);
			}//if
			else if (E is Win32Exception)
			{
				Win32Exception hE = (Win32Exception)E;
				int iError = hE.NativeErrorCode;
				if (Enum.IsDefined(typeof(Win32EC), iError))
					Ret.Add(Enum.GetName(typeof(Win32EC), iError));
				else
					Ret.Add(iError.ToString());
			}//if
			else
			{
//				Ret.Add(E.Message);
			}//else

			return Ret;
		}//func
	}//class
}//ns

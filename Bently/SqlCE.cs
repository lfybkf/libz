using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	public class SqlCE: IStoreSQL
	{
		SqlCeConnection NewConnection { get { return new SqlCeConnection(_ConnectionString ?? defaultConnectionString); } }
		public static string defaultConnectionString;
		string _ConnectionString;
		public string File	{ set { _ConnectionString = "Data Source=File;Persist Security Info=False;".Replace("File", value); } }
		public string ConnectionString	{ set { _ConnectionString = value; } }
		static Exception exceptionLast = null;
		public Exception LastError { get { return exceptionLast; } }
		public DbCommand getCommand() { return new SqlCeCommand(); }
		public DbParameter getParameter() { return new SqlCeParameter(); }

		public bool TestConnection(string testConnectionString) { try { var c = new SqlCeConnection(testConnectionString); c.Open(); c.Close(); return true; } catch { return false; } }

		public bool Execute(DbCommand cmd)
		{
			bool Ret;
			cmd.Connection = NewConnection;
			try
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
				Ret = true;
			}
			catch (Exception E)
			{
				exceptionLast = E;
				Ret = false;
			}
			finally
			{
				cmd.Connection.Close();
			}
			return Ret;
		}//function

		public DbDataReader OpenReader(DbCommand cmd)
		{
			SqlCeDataReader Ret = null;
			cmd.Connection = NewConnection;
			cmd.Connection.Open();
			try
			{
				Ret = (cmd as SqlCeCommand).ExecuteReader(CommandBehavior.CloseConnection);
			}
			catch (Exception E)
			{
				exceptionLast = E;
			}
			finally
			{
				//will close itself, when reader is closed
				//Conn.Close();
			}
			return Ret;
		}//function
	}//class
}//ns

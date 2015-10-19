using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDB;

namespace BDB
{
	public class SqlExpress: IStoreSQL
	{
		public static string defaultConnectionString;
		string _ConnectionString;
		public string ConnectionString { set { _ConnectionString = value; } }
		SqlConnection NewConnection { get { return new SqlConnection(_ConnectionString ?? defaultConnectionString); } }

		static Exception exceptionLast = null;
		public Exception LastError { get { return exceptionLast; } }
		public int rows = 0;

		public DbCommand getCommand() { return new SqlCommand(); }
		public DbParameter getParameter() { return new SqlParameter(); }

		public bool TestConnection(string testConnectionString) { try { var c = new SqlConnection(testConnectionString); c.Open(); c.Close(); return true; } catch { return false; } }


		public bool Execute(DbCommand cmd)
		{
			bool Ret;
			cmd.Connection = NewConnection;
			try
			{
				cmd.Connection.Open();
				rows = cmd.ExecuteNonQuery();
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

		}

		public DbDataReader OpenReader(DbCommand cmd)
		{
			SqlDataReader Ret = null;
			cmd.Connection = NewConnection;
			cmd.Connection.Open();
			try
			{
				Ret = (cmd as SqlCommand).ExecuteReader(CommandBehavior.CloseConnection);
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

		}

	}//class
}//ns

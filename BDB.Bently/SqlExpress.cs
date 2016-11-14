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
	///<summary>qlExpress: IStoreSQL</summary> 
	public class SqlExpress: IStoreSQL
	{
		public string Name
		{
			set { ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog={0};Integrated Security=True".fmt(value) ; }
		}
		string _ConnectionString;
		public string ConnectionString { set { _ConnectionString = value; } get { return _ConnectionString; } }
		SqlConnection NewConnection { get { return new SqlConnection(_ConnectionString); } }

		static Exception exceptionLast = null;
		public Exception LastError { get { return exceptionLast; } set { exceptionLast = value; } }
		public int rows { get; private set; }

		public DbCommand getCommand() { return new SqlCommand(); }
		public DbParameter getParameter() { return new SqlParameter(); }

		public bool TestConnection() { try { var c = new SqlConnection(ConnectionString); c.Open(); c.Close(); return true; } catch { return false; } }

		public void Connect(DbCommand cmd) { cmd.Connection = NewConnection; }

		///<summary>Execute</summary>
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
		///<summary>OpenReader</summary>
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

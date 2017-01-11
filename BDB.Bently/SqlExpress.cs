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
	///<summary>SqlExpress: IStoreSQL</summary> 
	public class SqlExpress: IStoreSQL
	{
		///<summary>see name</summary>
		public string Name
		{
			set { ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog={0};Integrated Security=True".fmt(value) ; }
		}
		string _ConnectionString;
		///<summary>see name</summary>
		public string ConnectionString { set { _ConnectionString = value; } get { return _ConnectionString; } }
		SqlConnection NewConnection { get { return new SqlConnection(_ConnectionString); } }

		static Exception exceptionLast = null;
		///<summary>see name</summary>
		public Exception LastError { get { return exceptionLast; } set { exceptionLast = value; } }
		///<summary>see name</summary>
		public int rows { get; private set; }
		///<summary>see name</summary>
		public DbCommand getCommand() { return new SqlCommand(); }
		///<summary>see name</summary>
		public DbParameter getParameter() { return new SqlParameter(); }
		///<summary>see name</summary>
		public bool TestConnection() { try { var c = new SqlConnection(ConnectionString); c.Open(); c.Close(); return true; } catch { return false; } }
		///<summary>see name</summary>
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

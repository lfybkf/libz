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
		public string Name
		{
			set {
				string file = System.IO.Directory.EnumerateFiles(
					Environment.CurrentDirectory,
					value + ".sdf",
					System.IO.SearchOption.AllDirectories).FirstOrDefault();
				File = file;
			}
		}
		string _ConnectionString;
		public string ConnectionString { set { _ConnectionString = value; } get { return _ConnectionString; } }
		SqlCeConnection NewConnection { get { return new SqlCeConnection(_ConnectionString); } }
		public string File { set { if (value != null) { _ConnectionString = "Data Source={0};Persist Security Info=False;".fmt(value); } } }

		static Exception exceptionLast = null;
		public Exception LastError { get { return exceptionLast; } }
		public DbCommand getCommand() { return new SqlCeCommand(); }
		public DbParameter getParameter() { return new SqlCeParameter(); }

		public bool TestConnection() { try { var c = new SqlCeConnection(ConnectionString); c.Open(); c.Close(); return true; } catch { return false; } }

		public SqlCE(string Name)
		{
			this.Name = Name;
		}//constructor

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

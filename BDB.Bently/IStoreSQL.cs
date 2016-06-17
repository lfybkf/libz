using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	public interface IStoreSQL
	{
		string Name { set; }
		string ConnectionString { set; get; }
		bool TestConnection();
		bool Execute(DbCommand cmd);
		void Connect(DbCommand cmd);
		DbCommand getCommand();
		DbParameter getParameter();
		DbDataReader OpenReader(DbCommand cmd);
		Exception LastError { get; set; }
	}//interface

	public static class Extension_IStoreSQL
	{
		public static DbCommand getCommand(this IStoreSQL store, string sql)
		{
			var cmd = store.getCommand();
			cmd.CommandText = sql;
			return cmd;
		}

		public static object Scalar(this IStoreSQL store, DbCommand cmd)
		{
			object result = null;
			store.Connect(cmd);
			try
			{
				cmd.Connection.Open();
				result = cmd.ExecuteScalar();
			}
			catch (Exception E)
			{
				store.LastError = E;
			}
			finally
			{
				cmd.Connection.Close();
			}
			return result;
		}//function

		
	}//class
}//ns

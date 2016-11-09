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

		public static DbParameter getParameter(this IStoreSQL store, string Name, object Value, bool IsNullable = false)
		{
			var result = store.getParameter();
			result.ParameterName = Name;
			if (IsNullable && Value == null)
			{
				result.Value = DBNull.Value;
			}//if
			else
			{
				result.Value = Value;
			}//else
			
			return result;
		}

		public static bool Execute(this IStoreSQL store, string sql)
		{
			var cmd = store.getCommand(sql);
			return store.Execute(cmd);
		}
		
		public static object Scalar(this IStoreSQL store, string sql)
		{
			var cmd = store.getCommand(sql);
			return store.Scalar(cmd);
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

		public static T Scalar<T>(this IStoreSQL store, DbCommand cmd, T def = default(T))
		{
			object result = store.Scalar(cmd);
			return result != null ? (T)Convert.ChangeType(result, typeof(T)) : def;
		}//function

		public static T Scalar<T>(this IStoreSQL store, string sql, T def = default(T))
		{
			var cmd = store.getCommand(sql);
			return store.Scalar<T>(cmd);
		}//function

		public static IEnumerable<T> Scalars<T>(this IStoreSQL store, string sql)
		{ return store.Scalars<T>(store.getCommand(sql)); }

		public static IEnumerable<T> Scalars<T>(this IStoreSQL store, DbCommand cmd)
		{
			List<T> result = new List<T>();
			object o;
			T item;
			DbDataReader reader = null;
			try
			{
				reader = store.OpenReader(cmd);
				while (reader.Read())
				{
					o = reader.GetValue(0);
					if (o != DBNull.Value)
					{
						item = (T)Convert.ChangeType(o, typeof(T));
						result.Add(item);
					}//if
				}//while
			}//try
			catch (Exception E)
			{
				store.LastError = E;
			}
			finally
			{
				reader.Close(); reader.Dispose();
			}
			return result;
		}//function

		public static T Max<T>(this IStoreSQL store, string Table, string Field, T def = default(T))
		{
			DbCommand cmd = store.getCommand(string.Format("SELECT MAX({0}) FROM {1}", Field, Table));
			return store.Scalar<T>(cmd, def);
		}//function
		
		public static T Min<T>(this IStoreSQL store, string Table, string Field, T def = default(T))
		{
			DbCommand cmd = store.getCommand(string.Format("SELECT MIN({0}) FROM {1}", Field, Table));
			return store.Scalar<T>(cmd, def);
		}//function
	}//class
}//ns

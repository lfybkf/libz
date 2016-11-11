using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	///<summary>интерфейс к БД (SQLce SQLexpress SQLserver)</summary>
	public interface IStoreSQL
	{
		///<summary>используется для построения ConnectionString</summary>
		string Name { set; }
		///<summary>прямая работа с ConnectionString</summary>
		string ConnectionString { set; get; }
		///<summary>проверка соединения</summary>
		bool TestConnection();
		///<summary>выполнить</summary>
		bool Execute(DbCommand cmd);
		///<summary>соединитсья</summary>
		void Connect(DbCommand cmd);
		///<summary>получить команду нужного типа (SqlCommand, SqlCeCommand...)</summary>
		DbCommand getCommand();
		///<summary>получить паарметр нужного типа</summary>
		DbParameter getParameter();
		///<summary>открыть ридер</summary>
		DbDataReader OpenReader(DbCommand cmd);
		///<summary>ошибка при последнем выполнении</summary> 
		Exception LastError { get; set; }
	}//interface

	///<summary>расширение для IStoreSQL</summary>
	public static class Extension_IStoreSQL
	{
		///<summary>получить команду и заполнить CommandText = sql</summary> 
		public static DbCommand getCommand(this IStoreSQL store, string sql)
		{
			var cmd = store.getCommand();
			cmd.CommandText = sql;
			return cmd;
		}


		///<summary>установить значение параметра. Если его нет, то добавить</summary> 
		public static void setParameter(this DbCommand cmd, string Name, object Value, bool IsNullable = false)
		{
			DbParameter parameter = null;
			if (cmd.Parameters.Contains(Name))
			{
				parameter = cmd.Parameters[Name];
			}//if
			else
			{
				parameter = cmd.CreateParameter();
				parameter.ParameterName = Name;
				cmd.Parameters.Add(parameter);
			}//else
			parameter.Value = (IsNullable && Value == null) ? DBNull.Value : Value;
		}//function


		///<summary>установить значение параметра. Если его нет, то добавить</summary> 
		public static void setParameter(this IStoreSQL store, DbCommand cmd, string Name, object Value, bool IsNullable = false)
		{
			cmd.setParameter(Name, Value, IsNullable);
		}

		///<summary>Получить заполненный параметр нужного типа. Если он может содержать NULL и Value==NULL, то значение параметра = DBNull.Value</summary> 
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
		///<summary>выполнить команду sql</summary> 
		public static bool Execute(this IStoreSQL store, string sql)
		{
			var cmd = store.getCommand(sql);
			return store.Execute(cmd);
		}

		///<summary>получить скаляр</summary>
		public static object Scalar(this IStoreSQL store, string sql)
		{
			var cmd = store.getCommand(sql);
			return store.Scalar(cmd);
		}

		///<summary>получить скаляр</summary>
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

		///<summary>получить скаляр и привести к типу Т</summary>
		public static T Scalar<T>(this IStoreSQL store, DbCommand cmd, T def = default(T))
		{
			object result = store.Scalar(cmd);
			return result != null ? (T)Convert.ChangeType(result, typeof(T)) : def;
		}//function

		///<summary>получить скаляр и привести к типу Т</summary>
		public static T Scalar<T>(this IStoreSQL store, string sql, T def = default(T))
		{
			var cmd = store.getCommand(sql);
			return store.Scalar<T>(cmd);
		}//function

		///<summary>получить массив скаляров и привести их к типу Т</summary>
		public static IEnumerable<T> Scalars<T>(this IStoreSQL store, string sql)
		{ return store.Scalars<T>(store.getCommand(sql)); }

		///<summary>получить массив скаляров и привести их к типу Т</summary>
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

		///<summary>получить максимум</summary> 
		public static T Max<T>(this IStoreSQL store, string Table, string Field, T def = default(T))
		{
			DbCommand cmd = store.getCommand(string.Format("SELECT MAX({0}) FROM {1}", Field, Table));
			return store.Scalar<T>(cmd, def);
		}//function
		///<summary>получить минимум</summary> 
		public static T Min<T>(this IStoreSQL store, string Table, string Field, T def = default(T))
		{
			DbCommand cmd = store.getCommand(string.Format("SELECT MIN({0}) FROM {1}", Field, Table));
			return store.Scalar<T>(cmd, def);
		}//function
	}//class
}//ns

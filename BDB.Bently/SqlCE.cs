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
	///<summary>sql compact</summary> 
	public class SqlCE: IStoreSQL
	{
		string _ConnectionString;
		///<summary>установка строки соединения</summary> 
		public string ConnectionString { set { _ConnectionString = value; } get { return _ConnectionString; } }
		SqlCeConnection NewConnection { get { return new SqlCeConnection(_ConnectionString); } }
		///<summary>или полный путь к файлу БД - или имя БД (без расширения) в текущем каталоге</summary> 
		public string Name { 
			set {
				string file;
				if (System.IO.File.Exists(value))
				{
					file = value;
				}//if
				else
				{
					file = System.IO.Directory.EnumerateFiles(
						Environment.CurrentDirectory,
						value + ".sdf",
						System.IO.SearchOption.AllDirectories).FirstOrDefault();
				}//else

				if (file != null)
				{ _ConnectionString = "Data Source={0};Persist Security Info=False;".fmt(file); }
				else
				{
					_ConnectionString = string.Empty;
				}//else
			} 
		}

		static Exception exceptionLast = null;
		///<summary>последняя ошибка</summary> 
		public Exception LastError { get { return exceptionLast; } set { exceptionLast = value; } }
		///<summary>SqlCeCommand</summary> 
		public DbCommand getCommand() { return new SqlCeCommand(); }
		///<summary>SqlCeParameter</summary> 
		public DbParameter getParameter() { return new SqlCeParameter(); }
		///<summary>проверка соединения</summary> 
		public bool TestConnection() { try { var c = new SqlCeConnection(ConnectionString); c.Open(); c.Close(); return true; } catch { return false; } }
		///<summary>соединиться</summary> 
		public void Connect(DbCommand cmd) { cmd.Connection = NewConnection; }
		///<summary>конструктор</summary> 
		public SqlCE(string Name)
		{
			this.Name = Name;
		}//constructor
		///<summary>выполнить команду</summary> 
		public bool Execute(DbCommand cmd)
		{
			bool Ret;
			Connect(cmd);
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

		///<summary>открыть читало</summary> 
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

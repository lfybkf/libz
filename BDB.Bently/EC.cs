using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	public class EC
	{
		#region properties
		public static IStoreSQL defaultStore;
		///<summary>по умолчанию =defaultStore. Но можно поменять у экземпляра</summary>
		public IStoreSQL store = defaultStore;

		public long ID;
		public long parentID;
		public IEntity entity = null;
		public ISet<IEntity> list;
		public string link;

		public Type type;
		#endregion

		public EC(){}//constructor
		public static EC New {get {return new EC();}}

		#region registry
		public class Registry
		{
			public Type type;
			public Action<IEntity, EC> BeforeRead;
			public Action<IEntity, EC> AfterRead;
			public Action<IEntity, EC> BeforeSave;
			public Action<IEntity, EC> AfterSave;
			public Action<IEntity, EC> BeforeDelete;
			public Action<IEntity, EC> AfterDelete;
			public Action<IEntity, EC> BeforeMaterialize;
			public Action<IEntity, EC> AfterMaterialize;
			public Func<IEntity, EC, bool> Validate;
		}//struct

		static List<Registry> registry = new List<Registry>();
		static Registry getRegistry(Type type) { return registry.FirstOrDefault(z => z.type == type); }
		static Registry getRegistry<T>() { return getRegistry(typeof(T)); }
		public static void RegisterType(Registry item) { registry.Add(item); }
		#endregion

		#region errors
		private List<Exception> errS = null;
		public void AddError(string msg) { LastError = new Exception(msg); }
		public Exception LastError
		{
			get { return errS.with(z => z.LastOrDefault());}
			set { 
				if (value == null) { return; } 
				errS = errS ?? new List<Exception>(); errS.Add(value); 
			}
		}
		#endregion

		#region paramS
		IDictionary<string, object> paramS = null;
		public void ParamSet (string ParamName, object ParamValue)
		{
			if (paramS == null) { paramS = new Dictionary<string, object>(); }
			paramS[ParamName] = ParamValue;
		}//function

		public object ParamGet(string ParamName)
		{
			if (paramS == null) { return null; }
			if (paramS.ContainsKey(ParamName) == false) { return null; }
			return paramS[ParamName];
		}//function

		public IEnumerable<string> ParamNames { get { return paramS == null ? new string[0] : paramS.Keys;} }
		#endregion

		bool Materialize(DbDataReader reader)
		{
			bool IsReaded = reader.Read();
			bool result = false;
			if (IsReaded)
			{
				var reg = getRegistry(type);
				runAction(reg.with(z => z.BeforeMaterialize));
				result = entity.Materialize(reader);
				if (result)
				{
					runAction(reg.with(z => z.AfterMaterialize));	
				}//if
			}//if
			return result;
		}//function

		private IEntity createEntity()
		{
			return (IEntity)Activator.CreateInstance(type);
		}//function

		///<summary>Загрузить из БД по ИД</summary>
		public T Read<T>(long ID) where T : class, IEntity
		{
			this.ID = ID;
			bool ok = Read();
			return ok ? this.entity as T : null;
		}//function

		///<summary>Загрузить из БД по entity.cmdRead</summary>
		public bool Read()
		{
			entity = createEntity();
			DbCommand cmd =  entity.cmdRead(this);
			if (cmd == null) { AddError("cmdRead is null"); return false; }

			bool result = true;
			DbDataReader reader = null;
			try
			{
				runAction(getRegistry(type).with(z => z.BeforeRead));
				reader = store.OpenReader(cmd);
				while (Materialize(reader)) { break; } //read one record
				runAction(getRegistry(type).with(z => z.AfterRead));
			}//try
			catch (Exception exception)
			{
				LastError = exception;
				result = false;
			}//catch
			finally { reader.Close(); reader.Dispose(); }
			return result;
		}//function

		///<summary>Сохранение entity</summary>
		public bool Save()
		{
			if (entity == null) { AddError("entity is null"); return false; }
			DbCommand cmd = entity.IsNew ? entity.cmdCreate(this) : entity.cmdUpdate(this);
			if (cmd == null) { AddError("cmdSave is null"); return false; }

			var Validate = getRegistry(type).with(z => z.Validate);
			bool IsValidated = Validate == null ? true : Validate(entity, this);
			bool result = true;
			try
			{
				runAction(getRegistry(type).with(z => z.BeforeSave));
				result = store.Execute(cmd);
				if (result)
				{
					runAction(getRegistry(type).with(z => z.AfterSave));	
				}//if
				else
				{
					LastError = store.LastError;
				}//else
				
			}//try
			catch (Exception exception)
			{
				LastError = exception;
				result = false;
			}//catch
			return result;
		}//function

		///<summary>считать произвольное целое. Default - значение по умолчанию</summary>
		public long Read(SqlBuilder sqlb, long Default)
		{
			long result = Default;
			DbCommand cmd = store.getCommand(sqlb.Select);
			var o = store.Scalar(cmd);
			if (o != null)
			{
				result = Convert.ToInt64(o);
			}//if
			return result;
		}//function

		///<summary>считать произвольную строку. Default - значение по умолчанию</summary>
		public string Read(SqlBuilder sqlb, string Default)
		{
			string result = Default;
			DbCommand cmd = store.getCommand(sqlb.Select);
			var o = store.Scalar(cmd);
			if (o != null)
			{
				result = Convert.ToString(o);
			}//if
			return result;
		}//function

		///<summary>update. Пока одного поля (ограничение SqlBuilder)</summary>
		public bool Update(SqlBuilder sqlb)
		{
			bool result = false;
			DbCommand cmd = store.getCommand(sqlb.Update);
			try
			{
				result = store.Execute(cmd);
			}//try
			catch (Exception exception)	{	LastError = exception; }//catch
			finally {  }//finally
			return result;
		}//function

		///<summary>Delete произвольный</summary>
		public bool Delete(SqlBuilder sqlb)
		{
			DbCommand cmd = store.getCommand(sqlb.Delete);
			bool result = false;
			try
			{
				result = store.Execute(cmd);
				if (result == false) { LastError = store.LastError; }//if
			}//try
			catch (Exception exception)	{ LastError = exception; }//catch
			return result;
		}//function

		///<summary>Delete entity с событиями</summary>
		public bool Delete()
		{
			DbCommand cmd = null;
			cmd = entity.cmdDelete(this);

			if (cmd == null) { AddError("cmdDelete is null"); return false; }

			bool result = false;
			try
			{
				runAction(getRegistry(type).with(z => z.BeforeDelete));
				result = store.Execute(cmd);
				if (result)
				{
					runAction(getRegistry(type).with(z => z.AfterDelete));	
				}//if
				else
				{
					LastError = store.LastError;
				}//else
			}//try
			catch (Exception exception)	{	LastError = exception;}//catch
			return result;
		}//function

		public long Max(string field, string table)
		{
			DbCommand cmd = store.getCommand();
			cmd.CommandText = "select MAX({0}) from {1}".fmt(field, table);
			object result = store.Scalar(cmd);
			return result == null ? long.MinValue : Convert.ToInt64(result);
		}//function

		public long Min(string field, string table)
		{
			DbCommand cmd = store.getCommand();
			cmd.CommandText = "select MIN({0}) from {1}".fmt(field, table);
			object result = store.Scalar(cmd);
			return result == null ? long.MaxValue : Convert.ToInt64(result);
		}//function

		///<summary>Select list</summary>
		public  bool Select(string sql)
		{
			DbCommand cmd = store.getCommand();
			cmd.CommandText = sql;
			return Select(cmd);
		}//function

		///<summary>Select list</summary>
		public bool Select(SqlBuilder sqlb)
		{
			DbCommand cmd = store.getCommand(sqlb.Select);
			return Select(cmd);
		}//function

		///<summary>Select list</summary>
		public bool  Select<T>(DbCommand cmd) where T : IEntity
		{
			type = typeof(T);
			return Select(cmd);
		}//function

		///<summary>Select list</summary>
		public bool Select<T>(string sql) where T : IEntity
		{
			type = typeof(T);
			return Select(sql);
		}//function

		///<summary>Заполнение списка из БД по комманде</summary>
		public bool Select(DbCommand cmd)
		{
			list = null;
			bool result = false;
			DbDataReader reader = null;
			try
			{
				reader = store.OpenReader(cmd);
				list = new HashSet<IEntity>();
				entity = createEntity();
				while (Materialize(reader)) 
				{
					list.Add(entity);
					entity = createEntity();
				}//while
				result = true;
			}//try
			catch (Exception exception)	{	LastError = exception; }//catch
			finally { reader.Close(); reader.Dispose(); }
			return result;

		}//function

		void runAction(Action<IEntity, EC> action)
		{
			if (action == null) { return; }
			action(entity, this);
		}//function
	}//class
}//ns

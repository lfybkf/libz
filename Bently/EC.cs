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
		public static IStoreSQL defaultStore;
		public IStoreSQL store = defaultStore;

		public long ID;
		public long parentID;
		public IEntity entity = null;
		public ISet<IEntity> list;
		public Func<EC, DbCommand> cmdSelect;
		public string link;

		private Type type;

		public EC(Type type)
		{
			this.type = type;
		}//constructor

		#region errors
		List<Exception> errS = null;
		public void AddError(string msg) { LastError = new Exception(msg); }
		public Exception LastError
		{
			get { if (errS == null) { return null; } return errS.LastOrDefault(); }
			set { if (errS == null) { errS = new List<Exception>(); } errS.Add(value); }
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

		public bool Load<T>() where T: IEntity
		{
			entity = (T)Activator.CreateInstance<T>(); 
			DbCommand cmd =  entity.cmdLoad(this);
			if (cmd == null) { AddError("cmdRead is null"); return false; }

			bool result = true;
			DbDataReader dbr = null;
			try
			{
				dbr = store.Select(cmd);
				if (dbr.HasRows == false)
				{
					result = false;
				}//if
				else
				{
					result = entity.Read(dbr);
				}//else
			}//try
			catch
			{
				result = false;
			}//catch
			finally { dbr.Close(); dbr.Dispose(); }
			return result;

		}//function

		public bool Save()
		{
			if (entity == null) { AddError("entity is null"); return false; }
			DbCommand cmd = entity.IsNew ? entity.cmdInsert(this) : entity.cmdUpdate(this);
			if (cmd == null) { AddError("cmdSave is null"); return false; }

			bool result = true;
			try
			{
				result = store.Execute(cmd);
			}//try
			catch (Exception exception)
			{
				LastError = exception;
				result = false;
			}//catch
			return result;
		}//function

		public bool Delete()
		{
			if (entity == null) { AddError("entity is null"); return false; }
			DbCommand cmd = entity.cmdDelete(this);
			if (cmd == null) { AddError("cmdDelete is null"); return false; }

			bool result = true;
			try
			{
				result = store.Execute(cmd);
			}//try
			catch (Exception exception)
			{
				LastError = exception;
				result = false;
			}//catch
			return result;
		}//function

		public bool Select<T>() where T:IEntity
		{
			list = null;
			if (cmdSelect == null) { AddError("cmdSelect is null"); return false; }

			bool result = true;
			DbDataReader dbr = null;
			try
			{
				dbr = store.Select(cmdSelect(this));
				if (dbr.HasRows == false)
				{
					result = false;
				}//if
				else
				{
					T item;
					while (dbr.Read())
					{
						item = (T)Activator.CreateInstance<T>();
						item.Read(dbr);
						list.Add(item);
					}//while
				}//else
			}//try
			catch
			{
				result = false;
			}//catch
			finally { dbr.Close(); dbr.Dispose(); }
			return result;

		}//function

	}//class
}//ns

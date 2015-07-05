﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	public class EC
	{
		public delegate DbCommand selectDelegate(IDictionary<string, object> paramS);

		static IStoreSQL store { get { return AC.Instance.StoreSQL; } }
		public A entity = null;
		public ISet<A> list;
		public selectDelegate cmdSelect;
		public string link;

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
		public void AddParam (string ParamName, object ParamValue)
		{
			if (paramS == null) { paramS = new Dictionary<string, object>(); }
			paramS[ParamName] = ParamValue;
		}//function
		#endregion

		public bool Load<T>(int ID) where T: A
		{
			entity = (A)Activator.CreateInstance(typeof(T));
			entity.ID = ID;
			DbCommand cmd = entity.cmdLoad;
			if (cmd == null) { AddError("cmdLoad is null"); return false; }

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
			DbCommand cmd = entity.IsNew ? entity.cmdInsert : entity.cmdUpdate;
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

		public bool Delete(int ID)
		{
			if (entity == null) { AddError("entity is null"); return false; }
			DbCommand cmd = entity.cmdDelete;
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

		public bool Select<T>() where T:A
		{
			list = null;
			if (cmdSelect == null) { AddError("cmdSelect is null"); return false; }

			bool result = true;
			DbDataReader dbr = null;
			try
			{
				dbr = store.Select(cmdSelect(paramS));
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

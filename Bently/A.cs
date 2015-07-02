using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BDB
{
	public abstract class A
	{
		protected static IStoreSQL store { get { return AC.Instance.StoreSQL; } }
		protected ulong ID = 0L;
		protected virtual bool IsValid { get { return true; } }

		protected virtual DbCommand cmdInsert { get {return null;} }
		protected virtual DbCommand cmdUpdate { get { return null; } }
		protected virtual DbCommand cmdLoad { get { return null; } }
		protected virtual DbCommand cmdDelete { get { return null; } }
		protected virtual bool Read(DbDataReader dbr) {return true;}

		#region errors
		List<Exception> errS = null;
		public void AddError(string msg) { LastError = new Exception(msg) ; }
		public Exception LastError { 
			get { if (errS == null) { return null; } return errS.LastOrDefault(); }
			set { if (errS == null) { errS = new List<Exception>(); } errS.Add(value); }
		}
		#endregion

		public bool Load(int aID)
		{
			bool result = false;
			DbDataReader dbr = null;
			DbCommand cmd = cmdLoad;
			if (cmd == null) { AddError("cmdLoad is null"); return false; }

			try
			{
				dbr = store.Select(cmd);
				if (dbr.HasRows == false)
				{
					result = false;
				}//if
				else
				{
					result = Read(dbr);
				}//else
			}//try
			catch
			{
				result = false;
			}//catch
			finally	{ dbr.Close(); }
			return result;
		}//function

		public bool Save()
		{
			DbCommand cmd = (ID == 0L) ? cmdInsert : cmdUpdate;
			if (cmd == null) { AddError("cmdSave is null"); return false; }

			bool result = true;
			try
			{
				store.Execute(cmd);
			}//try
			catch (Exception exception)
			{
				LastError = exception;
				result = false;
			}//catch
			return result;
		}//function

	}//class
}//ns

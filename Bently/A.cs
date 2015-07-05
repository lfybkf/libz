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
		public long ID { get; protected internal set; }
		public bool IsNew { get { return ID > 0; } }
		public virtual bool IsValid { get { return true; } }

		public virtual DbCommand cmdInsert { get {return null;} }
		public virtual DbCommand cmdUpdate { get { return null; } }
		public virtual DbCommand cmdLoad { get { return null; } }
		public virtual DbCommand cmdDelete { get { return null; } }
		public virtual bool Read(DbDataReader ddr) {return true;}
	}//class
}//ns

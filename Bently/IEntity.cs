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
	public interface IEntity
	{
		bool IsNew { get; }

		DbCommand cmdInsert(EC entityContext);
		DbCommand cmdUpdate(EC entityContext);
		DbCommand cmdLoad(EC entityContext);
		DbCommand cmdDelete(EC entityContext);
		bool Read(DbDataReader ddr);
	}//class
}//ns

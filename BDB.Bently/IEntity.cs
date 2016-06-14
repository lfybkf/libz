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

		DbCommand cmdCreate(EC entityContext);
		DbCommand cmdUpdate(EC entityContext);
		DbCommand cmdRead(EC entityContext);
		DbCommand cmdDelete(EC entityContext);
		bool Materialize(DbDataReader ddr);
	}//interface

}//ns

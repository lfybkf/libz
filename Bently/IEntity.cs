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
		long ID { get; set; }
		bool IsValid { get; }

		DbCommand cmdInsert { get; }
		DbCommand cmdUpdate { get; }
		DbCommand cmdRead { get; }
		DbCommand cmdDelete { get; }
		bool Read(DbDataReader ddr);
	}//class

	public static class IEntityExtension
	{
		public static bool IsNew(this IEntity entity)
		{
			return (entity.ID > 0);
		}//function
	}//class
}//ns

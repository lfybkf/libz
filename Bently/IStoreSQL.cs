﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	public interface IStoreSQL
	{
		bool Connect(string ConnectionString);
		bool Execute(DbCommand cmd);
		DbCommand getCommand();
		DbParameter getParameter();
		DbDataReader Select(DbCommand cmd);
		Exception LastError {get; }
	}//interface
}//ns

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BDB
{
	public static class SqlDataReaderExtension
	{
		public static int Get(this SqlDataReader sdr, string Col, int Def)
		{
			int i = sdr.GetOrdinal(Col);
			if (sdr.IsDBNull(i))
				return Def;
			else
				return sdr.GetInt32(i);
		}//func

		public static string Get(this SqlDataReader sdr, string Col, string Def)
		{
			int i = sdr.GetOrdinal(Col);
			if (sdr.IsDBNull(i))
				return Def;
			else
				return sdr.GetString(i);
		}//func

		public static decimal Get(this SqlDataReader sdr, string Col, decimal Def)
		{
			int i = sdr.GetOrdinal(Col);
			if (sdr.IsDBNull(i))
				return Def;
			else
			{
				return sdr.GetDecimal(i);
			}//else
		}//func
		
		public static bool Get(this SqlDataReader sdr, string Col, bool Def)
		{
			int i = sdr.GetOrdinal(Col);
			if (sdr.IsDBNull(i))
				return Def;
			else
				return sdr.GetBoolean(i);
		}//func

		public static DateTime Get(this SqlDataReader sdr, string Col, DateTime Def)
		{
			int i = sdr.GetOrdinal(Col);
			if (sdr.IsDBNull(i))
				return Def;
			else
				return sdr.GetDateTime(i);
		}//func
	}//class
}//ns

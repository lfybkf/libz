

//Generated 01.11.2015 13:33:26

using System;
using System.Data.Common;

namespace BDB
{
	public static class DbDataReaderExtension
	{
		public static string GetString(this DbDataReader reader, string Col)
		{
			int i = reader.GetOrdinal(Col);
			return reader.GetString(i);
		}//func

		public static string GetStringNullable(this DbDataReader reader, string Col)
		{
			int i = reader.GetOrdinal(Col);
			if (reader.IsDBNull(i))
				return null;
			else
				return reader.GetString(i);
		}//func

		public static string Get(this DbDataReader reader, string Col, string Def)
		{
			int i = reader.GetOrdinal(Col);
			if (reader.IsDBNull(i))
				return Def;
			else
				return reader.GetString(i);
		}//func

/// <summary>
/// Считать значение из колонки. Если IsDBNull, то возвращается Def
/// </summary>
/// <param name="Col">Имя колонки</param>
/// <param name="Def">Значение по умолчанию</param>
public static int? Get(this DbDataReader reader, string Col, int? Def)
{
	int i = reader.GetOrdinal(Col);
	if (reader.IsDBNull(i))
		return Def;
	else
		return reader.GetInt32(i);
}//func

/// <summary>
/// Считать значение из колонки без проверки на IsDBNull
/// </summary>
/// <param name="Col">Имя колонки</param>
/// <param name="Def">Значение по умолчанию (используется для overload)</param>
public static int Get(this DbDataReader reader, string Col, int Def)
{
	int i = reader.GetOrdinal(Col);
	return reader.GetInt32(i);
}//func

/// <summary>
/// Считать значение из колонки. Если IsDBNull, то возвращается null
/// </summary>
/// <param name="Col">Имя колонки</param>
public static int? GetInt32Nullable(this DbDataReader reader, string Col)
{
	int i = reader.GetOrdinal(Col);
	if (reader.IsDBNull(i))
		return null;
	else
		return reader.GetInt32(i);
}//func

/// <summary>
/// Считать значение из колонки. Без проверки IsDBNull
/// </summary>
/// <param name="Col">Имя колонки</param>
public static int GetInt32(this DbDataReader reader, string Col)
{
	int i = reader.GetOrdinal(Col);
	return reader.GetInt32(i);
}//func

/// <summary>
/// Считать значение из колонки. Если IsDBNull, то возвращается Def
/// </summary>
/// <param name="Col">Имя колонки</param>
/// <param name="Def">Значение по умолчанию</param>
public static long? Get(this DbDataReader reader, string Col, long? Def)
{
	int i = reader.GetOrdinal(Col);
	if (reader.IsDBNull(i))
		return Def;
	else
		return reader.GetInt64(i);
}//func

/// <summary>
/// Считать значение из колонки без проверки на IsDBNull
/// </summary>
/// <param name="Col">Имя колонки</param>
/// <param name="Def">Значение по умолчанию (используется для overload)</param>
public static long Get(this DbDataReader reader, string Col, long Def)
{
	int i = reader.GetOrdinal(Col);
	return reader.GetInt64(i);
}//func

/// <summary>
/// Считать значение из колонки. Если IsDBNull, то возвращается null
/// </summary>
/// <param name="Col">Имя колонки</param>
public static long? GetInt64Nullable(this DbDataReader reader, string Col)
{
	int i = reader.GetOrdinal(Col);
	if (reader.IsDBNull(i))
		return null;
	else
		return reader.GetInt64(i);
}//func

/// <summary>
/// Считать значение из колонки. Без проверки IsDBNull
/// </summary>
/// <param name="Col">Имя колонки</param>
public static long GetInt64(this DbDataReader reader, string Col)
{
	int i = reader.GetOrdinal(Col);
	return reader.GetInt64(i);
}//func

/// <summary>
/// Считать значение из колонки. Если IsDBNull, то возвращается Def
/// </summary>
/// <param name="Col">Имя колонки</param>
/// <param name="Def">Значение по умолчанию</param>
public static decimal? Get(this DbDataReader reader, string Col, decimal? Def)
{
	int i = reader.GetOrdinal(Col);
	if (reader.IsDBNull(i))
		return Def;
	else
		return reader.GetDecimal(i);
}//func

/// <summary>
/// Считать значение из колонки без проверки на IsDBNull
/// </summary>
/// <param name="Col">Имя колонки</param>
/// <param name="Def">Значение по умолчанию (используется для overload)</param>
public static decimal Get(this DbDataReader reader, string Col, decimal Def)
{
	int i = reader.GetOrdinal(Col);
	return reader.GetDecimal(i);
}//func

/// <summary>
/// Считать значение из колонки. Если IsDBNull, то возвращается null
/// </summary>
/// <param name="Col">Имя колонки</param>
public static decimal? GetDecimalNullable(this DbDataReader reader, string Col)
{
	int i = reader.GetOrdinal(Col);
	if (reader.IsDBNull(i))
		return null;
	else
		return reader.GetDecimal(i);
}//func

/// <summary>
/// Считать значение из колонки. Без проверки IsDBNull
/// </summary>
/// <param name="Col">Имя колонки</param>
public static decimal GetDecimal(this DbDataReader reader, string Col)
{
	int i = reader.GetOrdinal(Col);
	return reader.GetDecimal(i);
}//func

/// <summary>
/// Считать значение из колонки. Если IsDBNull, то возвращается Def
/// </summary>
/// <param name="Col">Имя колонки</param>
/// <param name="Def">Значение по умолчанию</param>
public static float? Get(this DbDataReader reader, string Col, float? Def)
{
	int i = reader.GetOrdinal(Col);
	if (reader.IsDBNull(i))
		return Def;
	else
		return reader.GetFloat(i);
}//func

/// <summary>
/// Считать значение из колонки без проверки на IsDBNull
/// </summary>
/// <param name="Col">Имя колонки</param>
/// <param name="Def">Значение по умолчанию (используется для overload)</param>
public static float Get(this DbDataReader reader, string Col, float Def)
{
	int i = reader.GetOrdinal(Col);
	return reader.GetFloat(i);
}//func

/// <summary>
/// Считать значение из колонки. Если IsDBNull, то возвращается null
/// </summary>
/// <param name="Col">Имя колонки</param>
public static float? GetFloatNullable(this DbDataReader reader, string Col)
{
	int i = reader.GetOrdinal(Col);
	if (reader.IsDBNull(i))
		return null;
	else
		return reader.GetFloat(i);
}//func

/// <summary>
/// Считать значение из колонки. Без проверки IsDBNull
/// </summary>
/// <param name="Col">Имя колонки</param>
public static float GetFloat(this DbDataReader reader, string Col)
{
	int i = reader.GetOrdinal(Col);
	return reader.GetFloat(i);
}//func

/// <summary>
/// Считать значение из колонки. Если IsDBNull, то возвращается Def
/// </summary>
/// <param name="Col">Имя колонки</param>
/// <param name="Def">Значение по умолчанию</param>
public static double? Get(this DbDataReader reader, string Col, double? Def)
{
	int i = reader.GetOrdinal(Col);
	if (reader.IsDBNull(i))
		return Def;
	else
		return reader.GetDouble(i);
}//func

/// <summary>
/// Считать значение из колонки без проверки на IsDBNull
/// </summary>
/// <param name="Col">Имя колонки</param>
/// <param name="Def">Значение по умолчанию (используется для overload)</param>
public static double Get(this DbDataReader reader, string Col, double Def)
{
	int i = reader.GetOrdinal(Col);
	return reader.GetDouble(i);
}//func

/// <summary>
/// Считать значение из колонки. Если IsDBNull, то возвращается null
/// </summary>
/// <param name="Col">Имя колонки</param>
public static double? GetDoubleNullable(this DbDataReader reader, string Col)
{
	int i = reader.GetOrdinal(Col);
	if (reader.IsDBNull(i))
		return null;
	else
		return reader.GetDouble(i);
}//func

/// <summary>
/// Считать значение из колонки. Без проверки IsDBNull
/// </summary>
/// <param name="Col">Имя колонки</param>
public static double GetDouble(this DbDataReader reader, string Col)
{
	int i = reader.GetOrdinal(Col);
	return reader.GetDouble(i);
}//func

/// <summary>
/// Считать значение из колонки. Если IsDBNull, то возвращается Def
/// </summary>
/// <param name="Col">Имя колонки</param>
/// <param name="Def">Значение по умолчанию</param>
public static bool? Get(this DbDataReader reader, string Col, bool? Def)
{
	int i = reader.GetOrdinal(Col);
	if (reader.IsDBNull(i))
		return Def;
	else
		return reader.GetBoolean(i);
}//func

/// <summary>
/// Считать значение из колонки без проверки на IsDBNull
/// </summary>
/// <param name="Col">Имя колонки</param>
/// <param name="Def">Значение по умолчанию (используется для overload)</param>
public static bool Get(this DbDataReader reader, string Col, bool Def)
{
	int i = reader.GetOrdinal(Col);
	return reader.GetBoolean(i);
}//func

/// <summary>
/// Считать значение из колонки. Если IsDBNull, то возвращается null
/// </summary>
/// <param name="Col">Имя колонки</param>
public static bool? GetBooleanNullable(this DbDataReader reader, string Col)
{
	int i = reader.GetOrdinal(Col);
	if (reader.IsDBNull(i))
		return null;
	else
		return reader.GetBoolean(i);
}//func

/// <summary>
/// Считать значение из колонки. Без проверки IsDBNull
/// </summary>
/// <param name="Col">Имя колонки</param>
public static bool GetBoolean(this DbDataReader reader, string Col)
{
	int i = reader.GetOrdinal(Col);
	return reader.GetBoolean(i);
}//func

/// <summary>
/// Считать значение из колонки. Если IsDBNull, то возвращается Def
/// </summary>
/// <param name="Col">Имя колонки</param>
/// <param name="Def">Значение по умолчанию</param>
public static DateTime? Get(this DbDataReader reader, string Col, DateTime? Def)
{
	int i = reader.GetOrdinal(Col);
	if (reader.IsDBNull(i))
		return Def;
	else
		return reader.GetDateTime(i);
}//func

/// <summary>
/// Считать значение из колонки без проверки на IsDBNull
/// </summary>
/// <param name="Col">Имя колонки</param>
/// <param name="Def">Значение по умолчанию (используется для overload)</param>
public static DateTime Get(this DbDataReader reader, string Col, DateTime Def)
{
	int i = reader.GetOrdinal(Col);
	return reader.GetDateTime(i);
}//func

/// <summary>
/// Считать значение из колонки. Если IsDBNull, то возвращается null
/// </summary>
/// <param name="Col">Имя колонки</param>
public static DateTime? GetDateTimeNullable(this DbDataReader reader, string Col)
{
	int i = reader.GetOrdinal(Col);
	if (reader.IsDBNull(i))
		return null;
	else
		return reader.GetDateTime(i);
}//func

/// <summary>
/// Считать значение из колонки. Без проверки IsDBNull
/// </summary>
/// <param name="Col">Имя колонки</param>
public static DateTime GetDateTime(this DbDataReader reader, string Col)
{
	int i = reader.GetOrdinal(Col);
	return reader.GetDateTime(i);
}//func

	}//class
}//ns

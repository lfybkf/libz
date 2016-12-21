
//Generated 21.12.2016 16:54:32

using System.Collections.Generic;
using System.Linq;
namespace BDB
{
///<summary>набор статических функций для SQL</summary>
public static class SqlF
{
	///<summary>соединить условия из списка по И</summary>
	public static string ANDed(IEnumerable<string> whereS) {return string.Join(" and ", whereS);}//function

	///<summary>соединить условия из списка по ИЛИ</summary>
	public static string ORed(IEnumerable<string> whereS) {	return string.Join(" or ", whereS);	}//function

	///<summary>fld IS NULL</summary>
	public static string IsNull(string fld) { return "{0} IS NULL".fmt(fld); }//function
	///<summary>fld IS NOT NULL</summary>
	public static string IsNotNull(string fld) { return "{0} IS NOT NULL".fmt(fld); }//function

	///<summary>fld In list</summary>
	public static string InList<T>(string fld, IEnumerable<T> list, bool IsIn = true)
	{
		string sList;
		if (typeof(T) == typeof(string))
		{
			sList = string.Join(S.Comma, list.Select(s => s.ToString().quote(C.Quote)));
		}//if
		else
		{
			sList = string.Join(S.Comma, list.Select(s => s.ToString()));
		}//else

		if (IsIn)
		{
			return "{0} IN ({1})".fmt(fld, sList);
		}//if
		else
		{
			return "{0} NOT IN ({1})".fmt(fld, sList);
		}//else
	}//function





		///<summary>fld Eq val</summary>
	public static string EqValue(string fld, object val) { return "{0} = {1}".fmt(fld, (val is string) ? val.ToString().quote(C.Quote) : val); }
		///<summary>fld Ge val</summary>
	public static string GeValue(string fld, object val) { return "{0} >= {1}".fmt(fld, (val is string) ? val.ToString().quote(C.Quote) : val); }
		///<summary>fld Gt val</summary>
	public static string GtValue(string fld, object val) { return "{0} > {1}".fmt(fld, (val is string) ? val.ToString().quote(C.Quote) : val); }
		///<summary>fld Le val</summary>
	public static string LeValue(string fld, object val) { return "{0} <= {1}".fmt(fld, (val is string) ? val.ToString().quote(C.Quote) : val); }
		///<summary>fld Ls val</summary>
	public static string LsValue(string fld, object val) { return "{0} < {1}".fmt(fld, (val is string) ? val.ToString().quote(C.Quote) : val); }
		///<summary>fld Like val</summary>
	public static string LikeValue(string fld, object val) { return "{0} LIKE {1}".fmt(fld, (val is string) ? val.ToString().quote(C.Quote) : val); }
		///<summary>fld LikeNot val</summary>
	public static string LikeNotValue(string fld, object val) { return "{0} NOT LIKE {1}".fmt(fld, (val is string) ? val.ToString().quote(C.Quote) : val); }
	
		///<summary>fld Eq par</summary>
	public static string EqPar(string fld, string par = null) { return "{0} = @{1}".fmt(fld, par ?? fld); }
		///<summary>fld Ge par</summary>
	public static string GePar(string fld, string par = null) { return "{0} >= @{1}".fmt(fld, par ?? fld); }
		///<summary>fld Gt par</summary>
	public static string GtPar(string fld, string par = null) { return "{0} > @{1}".fmt(fld, par ?? fld); }
		///<summary>fld Le par</summary>
	public static string LePar(string fld, string par = null) { return "{0} <= @{1}".fmt(fld, par ?? fld); }
		///<summary>fld Ls par</summary>
	public static string LsPar(string fld, string par = null) { return "{0} < @{1}".fmt(fld, par ?? fld); }
		///<summary>fld Like par</summary>
	public static string LikePar(string fld, string par = null) { return "{0} LIKE @{1}".fmt(fld, par ?? fld); }
		///<summary>fld LikeNot par</summary>
	public static string LikeNotPar(string fld, string par = null) { return "{0} NOT LIKE @{1}".fmt(fld, par ?? fld); }
	}//class
}//ns

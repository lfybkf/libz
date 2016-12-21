
//Generated 21.12.2016 20:02:36

using System.Collections.Generic;
using System.Linq;
namespace BDB
{
///<summary>набор статических функций для SQL</summary>
public static partial class SqlF
{
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

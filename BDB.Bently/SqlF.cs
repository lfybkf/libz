
//Generated 23.12.2016 0:19:06

using System.Collections.Generic;
using System.Linq;
namespace BDB
{
///<summary>набор статических функций для SQL</summary>
public static partial class SqlF
{
		///<summary>fld Eq val</summary>
	public static string Eq(string fld, object val) { return "{0} = {1}".fmt(fld, (val is string) ? val.ToString().quote(C.Quote) : val); }
		///<summary>fld No val</summary>
	public static string No(string fld, object val) { return "{0} != {1}".fmt(fld, (val is string) ? val.ToString().quote(C.Quote) : val); }
		///<summary>fld Ge val</summary>
	public static string Ge(string fld, object val) { return "{0} >= {1}".fmt(fld, (val is string) ? val.ToString().quote(C.Quote) : val); }
		///<summary>fld Gt val</summary>
	public static string Gt(string fld, object val) { return "{0} > {1}".fmt(fld, (val is string) ? val.ToString().quote(C.Quote) : val); }
		///<summary>fld Le val</summary>
	public static string Le(string fld, object val) { return "{0} <= {1}".fmt(fld, (val is string) ? val.ToString().quote(C.Quote) : val); }
		///<summary>fld Ls val</summary>
	public static string Ls(string fld, object val) { return "{0} < {1}".fmt(fld, (val is string) ? val.ToString().quote(C.Quote) : val); }
	
		///<summary>fld Eq par</summary>
	public static string EqPar(string fld, string par = null) { return "{0} = @{1}".fmt(fld, par ?? fld); }
		///<summary>fld No par</summary>
	public static string NoPar(string fld, string par = null) { return "{0} != @{1}".fmt(fld, par ?? fld); }
		///<summary>fld Ge par</summary>
	public static string GePar(string fld, string par = null) { return "{0} >= @{1}".fmt(fld, par ?? fld); }
		///<summary>fld Gt par</summary>
	public static string GtPar(string fld, string par = null) { return "{0} > @{1}".fmt(fld, par ?? fld); }
		///<summary>fld Le par</summary>
	public static string LePar(string fld, string par = null) { return "{0} <= @{1}".fmt(fld, par ?? fld); }
		///<summary>fld Ls par</summary>
	public static string LsPar(string fld, string par = null) { return "{0} < @{1}".fmt(fld, par ?? fld); }
	}//class
}//ns

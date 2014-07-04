using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDB
{
	public static class ListExtension
	{
		public static Kommand get(this IEnumerable<Kommand> lst, Keys scut) { return lst.get(k => k.Scut == scut); }//func
		public static Kommand get(this IEnumerable<Kommand> lst, string Name) { return lst.get(k => k.Name == Name); }//func

		public static Kommand get(this IEnumerable<Kommand> lst, Func<Kommand, bool> Match)
		{
			Kommand k = lst.FirstOrDefault(Match);
			return k ?? Kommand.Default;
		}//func
	}//class
}//ns

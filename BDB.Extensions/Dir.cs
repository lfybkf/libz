using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	///<summary>see name</summary>
	public static class Dir
	{
		///<summary>create if not exists</summary> 
		public static DirectoryInfo garant(this DirectoryInfo dir)
		{
			if (dir.Exists == false) { dir.Create(); }
			return dir;
		}//function
		
		///<summary>subPath</summary>
		public static string subPath(this DirectoryInfo dir, string path) => path.afterCI(dir.FullName);

		/// <summary>формирует Path из массива строк</summary>
		public static string add(this DirectoryInfo dir, params string[] args)
		{
			string s = dir.FullName;
			foreach (string path in args)
			{
				s = Path.Combine(s, path);
			}//for
			return s;
		}//func
	}//class
}

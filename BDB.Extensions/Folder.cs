using System;
using System.Collections.Generic;
using io = System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	///<summary>path с обязательным слэшем(\) в конце</summary>
	public sealed class Folder
	{
		string path;
		private Folder() {	}//constructor
		static char cSlash = io.Path.DirectorySeparatorChar;

		///<summary>Path</summary> 
		public string Path => path;
		///<summary>exists?</summary> 
		public bool Exists => io.Directory.Exists(path);
		///<summary>Parent</summary> 
		public string Parent => (Root == Path) ? null : path.AsEnumerable().Take(path.Length-1).takeUntilLast(ch => ch == cSlash).toString();
		///<summary>Root</summary> 
		public string Root => io.Directory.GetDirectoryRoot(path);
		///<summary>Folder[C:\Temp].Subpath(C:\Temp\dir1\dir2) = dir1\dir2</summary>
		public string SubPath(string aPath) => aPath.afterCI(path);
		///<summary>Folder[C:\Temp].Subpath(C:\Temp\dir1\dir2) = dir1\dir2</summary>
		public string SubPath(Folder fld) => SubPath(fld.Path);
		///<summary>minus</summary>
		public static string operator-(Folder fld1, Folder fld2) => (fld1.path.Length < fld2.path.Length) ? fld1.SubPath(fld2) : fld2.SubPath(fld1);
		///<summary>minus</summary>
		public static string operator-(Folder fld, string path) => (fld.path.Length < path.Length) ? fld.SubPath(path) : fld.path.afterCI(path);
		///<summary>to string</summary> 
		public override string ToString() => path;
		///<summary>get files</summary>
		public IEnumerable<string> Files => io.Directory.EnumerateFiles(path);
		///<summary>get files AllDirectories</summary>
		public IEnumerable<string> FilesAll => io.Directory.EnumerateFiles(path, "*.*", io.SearchOption.AllDirectories);
		///<summary>get directories</summary>
		public IEnumerable<string> Directories => io.Directory.EnumerateDirectories(path);

		///<summary>Create</summary> 
		public Folder Create() {
			if (io.Directory.Exists(path) == false) { io.Directory.CreateDirectory(path); }
			return this;
		}

		///<summary>перевести на новый корень</summary> 
		public void ReRoot(string aPath)
		{
			string root = io.Directory.GetDirectoryRoot(aPath);
			string myRoot = this.Root;
			if (root != myRoot)
			{
				var sub = this - myRoot;
				var newPath = io.Path.Combine(root, sub);
				path = newPath;
			}//if
		}//function


		///<summary>constructor с гарантией (опцион)</summary> 
		public static Folder New (string path, bool toCreate = true)
		{
			Folder result = new Folder();

			if (path.LastIndexOf(cSlash) == path.Length - 1)
			{
				result.path = path;
			}//if
			else
			{
				result.path = path + cSlash;
			}//else

			result.Create();
			return result;
		}//constructor

		/// <summary>формирует Path из массива строк</summary>
		public string Add(params string[] args)
		{
			string s = this.path;
			foreach (string path in args)
			{
				s = io.Path.Combine(s, path);
			}//for
			return s;
		}//func
	}//class
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using io = System.IO;

namespace BDB
{
	///<summary>ini-file</summary>
	public class Ini
	{
		private Ini(string path)
		{
			this.path = path;
			dict = new Dictionary<string, string>();
		}//constructor

		string path { get; set; }
		Dictionary<string, string> dict;

		///<summary>is empty</summary> 
		public bool IsEmpty => dict.isEmpty();

		///<summary>стереть содержимое</summary>
		public void Clear() => dict.Clear();

		///<summary>load</summary>
		public void Fill(IEnumerable<string> lines)
		{
			if (lines.isEmpty()) { return; }
			foreach (var line in lines.Where(z => z.Contains(S.Eq)))
			{
				dict[line.before(S.Eq)] = line.after(S.Eq);
			}//for
		}//function

		///<summary>load</summary>
		public static Ini Load(string path, bool toCreate = false)
		{
			if (path.isEmpty()) { return null; }
			Ini result = null;
			if (io.File.Exists(path))
			{
				result = new Ini(path);
				var lines = io.File.ReadAllLines(path);
				result.Fill(lines);
			}//if
			else
			{
				if (toCreate)
				{
					result = new Ini(path);
					result.Fill(null);
				}//if
			}//else
			return result;
		}//function

		///<summary>save</summary>
		public void Save()
		{
			var lines = dict.Keys.Select(k => $"{k}={dict[k]}");
			io.File.WriteAllLines(path, lines);
		}//function

		///<summary>dictionary access</summary>
		public string this[string key] { get { return dict.get(key); } set { dict[key] = value;	} }
		///<summary>get</summary>
		public int get(string key, int def) => dict.get(key).with(z => z.parse(def), def);
		///<summary>set</summary>
		public void set(string key, int val) => dict[key] = val.ToString();
		///<summary>get</summary>
		public string get(string key, string def) => dict.get(key).with(z => z, def);
		///<summary>set</summary>
		public void set(string key, string val) => dict[key] = val;
		///<summary>get через ;</summary>
		public string[] get(string key, string[] def) => dict.get(key).with(z => z.splitSemicolon(), def);
		///<summary>set через ;</summary>
		public void set(string key, string[] val) => dict[key] = string.Join(S.Semicolon, val);
		///<summary>get int</summary> 
		public int getInt(string key) => get(key, default(int));
		///<summary>get str</summary> 
		public string getString(string key) => get(key, default(string));
		///<summary>get int</summary> 
		public string[] getArray(string key) => get(key, default(string[]));
	}//class
}

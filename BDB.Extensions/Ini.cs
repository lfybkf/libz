using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
			var lines = dict.Keys.ordered().Select(k => $"{k}={dict[k]}");
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

		///<summary>deserialize</summary> 
		public T DeSerialize<T>()
		{
			T res = Activator.CreateInstance<T>();
			Type t = typeof(T);
			PropertyInfo[] props = t.GetProperties();
			foreach (var prop in props)
			{
				string name = prop.Name;
				Type type = prop.PropertyType;
				if (type == typeof(int))
				{
					prop.SetValue(res, get(name, default(int)));
				}//if
				else if (type == typeof(string))
				{
					prop.SetValue(res, get(name, default(string)));
				}//else
				else if (type == typeof(string[]))
				{
					prop.SetValue(res, get(name, default(string[])));
				}//else
			}//for
			return res;
		}//function

		///<summary>serialize</summary> 
		public void Serialize(object obj)
		{
			Type t = obj.GetType();
			PropertyInfo[] props = t.GetProperties();

			foreach (var prop in props)
			{
				string name = prop.Name;
				Type type = prop.PropertyType;
				if (type == typeof(int))
				{
					set(name, (int)prop.GetValue(obj));
				}//if
				else if (type == typeof(string))
				{
					set(name, (string)prop.GetValue(obj));
				}//else
				else if (type == typeof(string[]))
				{
					set(name, (string[])prop.GetValue(obj));
				}//else
			}//for
		}//function
	}//class
}

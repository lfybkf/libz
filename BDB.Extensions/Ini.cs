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
		#region IDict
		///<summary>для записей не связанных со свойствами</summary> 
		public interface IDict {
			///<summary>get</summary> 
			string get(string key);
			///<summary>set</summary> 
			void set(string key, string val);
			///<summary>ключи несвойств</summary>
			IEnumerable<string> keys { get; }
		}
		#endregion

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

		///<summary>есть ли ключ</summary>
		public bool Has(string key) => dict.ContainsKey(key);

		///<summary>ключи</summary>
		public IEnumerable<string> Keys => dict.Keys.AsEnumerable();

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
		///<summary>get</summary>
		public bool get(string key, bool def) => dict.get(key).with(z => !z.equalCI("false"), def);
		///<summary>set</summary>
		public void set(string key, bool val) => dict[key] = val.ToString();
		///<summary>get через ;</summary>
		public string[] get(string key, string[] def) => dict.get(key).with(z => z.splitSemicolon(), def);
		///<summary>set через ;</summary>
		public void set(string key, string[] val) => dict[key] = string.Join(S.Semicolon, val);
		///<summary>get int</summary>
		public int getInt(string key) => get(key, default(int));
		///<summary>get str</summary>
		public string getString(string key) => get(key, default(string));
		///<summary>get bool</summary>
		public bool getBool(string key) => get(key, false);
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
				if (!Has(name)) { continue; }
				Type type = prop.PropertyType;

				if (type == typeof(int))
				{
					prop.SetValue(res, getInt(name));
				}//if
				else if (type == typeof(bool)) //По умолчанию свойство должно быть false.
				{
					prop.SetValue(res, getBool(name));
				}//else
				else if (type == typeof(string))
				{
					prop.SetValue(res, getString(name));
				}//else
				else if (type == typeof(string[]))
				{
					prop.SetValue(res, getArray(name));
				}//else
			}//for

			if (res is IDict)
			{
				IDict d = res as IDict;
				foreach (var key in Keys.Except(props.Select(p => p.Name)))
				{
					d.set(key, getString(key));
				}//for
			}//if
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
				else if (type == typeof(bool))
				{
					set(name, (bool)prop.GetValue(obj));
				}//else
				else if (type == typeof(string[]))
				{
					set(name, (string[])prop.GetValue(obj));
				}//else
			}//for
		}//function
	}//class
}

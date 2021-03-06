﻿#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB.Templating
{
	///<summary></summary> 
	public abstract class AttrzME: Attrz //MachineElement
	{
		public virtual string Name {get; set;}
		public string Info;
		public override string ToString() { return "{0} ({1})".fmt(Name, Info); }

		internal string MachineName;
		public Machine machine { get { return Map.Current.machines.FirstOrDefault(m => m.Name == MachineName); } }

		private List<Comment> _comments = new List<Comment>();
		public IEnumerable<Comment> comments {get {return _comments;}}

		public static string[] EmptyStrings = { };
		protected Lazy<List<string>> _errors = new Lazy<List<string>>();
		protected Lazy<List<string>> _warnings = new Lazy<List<string>>();
		public IEnumerable<string> errors { get { return _errors.IsValueCreated ? _errors.Value : null; } }
		public IEnumerable<string> warnings { get { return _warnings.IsValueCreated ? _warnings.Value : null; } }
		protected void addError(string s) { _errors.Value.Add(s); }
		protected void addWarning(string s) { _warnings.Value.Add(s); }
		protected void addErrors(IEnumerable<string> ss) { if (ss != null) { _errors.Value.AddRange(ss); } }
		protected void addWarnings(IEnumerable<string> ss) { if (ss != null) { _warnings.Value.AddRange(ss); } }
		public bool hasErrors { get { return _errors.IsValueCreated; } }
		public bool hasWarnings { get { return _warnings.IsValueCreated; } }
		public virtual bool Validate() { return true; }

		internal override void Read(System.Xml.Linq.XElement src)
		{
			Name = Get(R.Name);
			Info = Gef(R.Info, string.Empty);
			FillListFromXlist<Comment>(_comments, src.Elements(R.Comment));
		}//function

		
	}//class
}//ns

#pragma warning restore 1591
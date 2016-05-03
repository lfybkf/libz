using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BDB
{
	public class Kommand
	{
		//event
		public event EventHandler isExecuted;
		protected void fireIsExecuted() { if (isExecuted != null) { isExecuted.Invoke(this, null); } }

		//public
		internal readonly string Name = String.Empty;
		internal readonly Keys keys = Keys.None;
		internal IList<Action> Execs = new List<Action>();
		public string Caption = String.Empty;
		public Shortcut shortCut { get { return getShortcut(keys); } }

		//static
		public static Kommand Empty = new Kommand();
		public static Kommand Default = Empty;

		//function
		private Kommand() { }

		public Kommand(string Name, Action Exec)
		{
			this.Name = Name;
			this.Caption = Name;
			this.keys = Keys.None;
			if (Exec != null)	Execs.Add(Exec);
		}//func

		public Kommand(string Name, Action Exec, Keys keys)
		{
			this.Name = Name;
			this.Caption = Name;
			this.keys = keys;
			if (Exec != null) Execs.Add(Exec);
		}//func

		public Kommand(string Name, string Caption, Action Exec, Keys keys)
		{
			this.Name = Name;
			this.Caption = Caption;
			this.keys = keys;
			if (Exec != null) Execs.Add(Exec);
		}//func

		public Kommand(string Name, Keys keys)
		{
			this.Name = Name;
			this.Caption = Name;
			this.keys = keys;
		}//func

		public Kommand Add(Action Exec)
		{
			if (Exec != null) Execs.Add(Exec);
			return this;
		}//functoin

		public void Execute()
		{
			foreach (var action in Execs)	{ action(); }
			fireIsExecuted();
		}//func

		public static void Execute(Kommand kmd)		{if (kmd != null) { kmd.Execute(); }}//func
		public override string ToString() { return Name; }//func

		public static void KommandEventHandler(Object o, EventArgs ea)
		{
			Kommand k = GetFromTag(o);
			Execute(k);
		}//function

		public static Kommand GetFromTag(object cmp)
		{
			Kommand Ret = null;

			if (cmp is ToolStripItem)
			{
				ToolStripItem ctl = (ToolStripItem)cmp;
				if (ctl.Tag is Kommand)
					Ret = (Kommand)ctl.Tag;
			}
			else if (cmp is Control)
			{
				Control ctl = (Control)cmp;
				if (ctl.Tag is Kommand)
					Ret = (Kommand)ctl.Tag;
			}

			return Ret;
		}//func		

		#region shortcut
		public static Shortcut getShortcut(Keys key)
		{
			if (key.HasFlag(Keys.Control))
			{
				if (key == Keys.A)				return Shortcut.CtrlA;
				else if (key == Keys.C)			return Shortcut.CtrlC;
				else if (key == Keys.E)			return Shortcut.CtrlE;
				else if (key == Keys.F)			return Shortcut.CtrlF;
				else if (key == Keys.H)			return Shortcut.CtrlH;
				else if (key == Keys.I)			return Shortcut.CtrlI;
				else if (key == Keys.O)			return Shortcut.CtrlO;
				else if (key == Keys.R)			return Shortcut.CtrlR;
				else if (key == Keys.S)			return Shortcut.CtrlS;
				else if (key == Keys.V)			return Shortcut.CtrlV;
				else if (key == Keys.Z)			return Shortcut.CtrlZ;
				else if (key == Keys.Delete)	return Shortcut.CtrlDel;
				else if (key == Keys.Insert)	return Shortcut.CtrlIns;
			}//if
			else if (key.HasFlag(Keys.Shift))
			{
				if (key == Keys.Insert)			return Shortcut.ShiftIns;
				else if (key == Keys.Delete)	return Shortcut.ShiftDel;
			}//if

			return Shortcut.None;
		}//func
		#endregion

	}//class

}//ns

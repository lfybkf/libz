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
		//public
		internal readonly string Name = String.Empty;
		internal readonly Action Exec = null;
		internal readonly Keys Scut = Keys.None;
		public string Caption = String.Empty;

		//static
		public static Kommand Empty = new Kommand();
		public static Kommand Default = Empty;
		public static String PrefixButton = "btn";
		public static String PrefixMenu = "mi";

		//function
		private Kommand() { }

		public Kommand(string Name, Action Exec)
		{
			this.Name = Name;
			this.Caption = Name;
			this.Exec = Exec;
			this.Scut = Keys.None;
		}//func

		public Kommand(string Name, Action Exec, Keys Scut)
		{
			this.Name = Name;
			this.Caption = Name;
			this.Exec = Exec;
			this.Scut = Scut;
		}//func

		public Kommand(string Name, string Caption, Action Exec, Keys Scut)
		{
			this.Name = Name;
			this.Caption = Caption;
			this.Exec = Exec;
			this.Scut = Scut;
		}//func

		public void Execute()
		{
			if (Exec != null)
			{
				Exec();
			}//if
		}//func

		public static void Execute(Kommand kmd)		{if (kmd != null) { kmd.Execute(); }}//func
		public override string ToString() { return Name; }//func

		public static void Assign(IEnumerable<Kommand> lst, Component cmp, EventHandler click)
		{
			string Name = string.Empty;
			Kommand kmd = null;

			if (cmp is ToolStripMenuItem)
			{
				ToolStripMenuItem ctl = (ToolStripMenuItem)cmp;
				kmd = lst.get(ctl.Name.After(PrefixMenu));
				if (kmd != null)
				{
					ctl.Text = kmd.Caption;
					ctl.Tag = kmd;
					ctl.Click += click;
					ctl.ShortcutKeys = kmd.Scut;
				}//if
			}
			else if (cmp is Button)
			{
				Button ctl = (Button)cmp;
				Name = ctl.Name.After(PrefixButton);
				kmd = lst.Where(k => k.Name == Name).ElementAtOrDefault(0);
				if (kmd != null)
				{
					ctl.Text = kmd.Caption;
					ctl.Tag = kmd;
					ctl.Click += click;
				}//if
			}
			else
				return;
		}//func		

		public static Kommand GetFromTag(object cmp)
		{
			Kommand Ret = null;

			if (cmp is ToolStripItem)
			{
				ToolStripItem ctl = (ToolStripItem)cmp;
				if (ctl.Tag is Kommand)
					Ret = (Kommand)ctl.Tag;
			}
			else if (cmp is Button)
			{
				Button ctl = (Button)cmp;
				if (ctl.Tag is Kommand)
					Ret = (Kommand)ctl.Tag;
			}

			return Ret;
		}//func		


		#region shortcut
		public static Shortcut GetShortcut(Keys key, Keys mods)
		{
			if (mods.HasFlag(Keys.Control))
			{
				if (key == Keys.C)
					return Shortcut.CtrlC;
				else if (key == Keys.O)
					return Shortcut.CtrlO;
				else if (key == Keys.S)
					return Shortcut.CtrlS;
				else if (key == Keys.V)
					return Shortcut.CtrlV;
				else if (key == Keys.Z)
					return Shortcut.CtrlZ;
				else if (key == Keys.Delete)
					return Shortcut.CtrlDel;
				else if (key == Keys.Insert)
					return Shortcut.CtrlIns;
			}//if
			else if (mods.HasFlag(Keys.Shift))
			{
				if (key == Keys.Insert)
					return Shortcut.ShiftIns;
				else if (key == Keys.Delete)
					return Shortcut.ShiftDel;
			}//if

			return Shortcut.None;
		}//func
		#endregion

	}//class

}//ns


using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDB
{
	public static class ListExtension
	{
		public static String PrefixButton = "btn";
		public static String PrefixMenu = "mi";

		public static Kommand get(this IEnumerable<Kommand> lst, Keys keys) { return lst.get(k => k.keys == keys); }//func
		public static Kommand get(this IEnumerable<Kommand> lst, string Name) { return lst.get(k => k.Name == Name); }//func

		public static Kommand get(this IEnumerable<Kommand> lst, Func<Kommand, bool> Match)
		{
			Kommand k = lst.FirstOrDefault(Match);
			return k ?? Kommand.Default;
		}//func


		public static void LinkToComponent(this IEnumerable<Kommand> list, Component cmp)
		{
			list.LinkToComponent(cmp, Kommand.KommandEventHandler);
		}//function

		public static void LinkToComponent(this IEnumerable<Kommand> list, Component cmp, EventHandler click)
		{
			Kommand kmd = null;

			if (cmp is ToolStripItem)
			{
				ToolStripItem ctl = (ToolStripItem)cmp;
				kmd = list.get(ctl.Name.after(PrefixMenu));
				if (kmd != Kommand.Default)
				{
					ctl.Text = kmd.Caption;
					ctl.Tag = kmd;
					ctl.Click += click;
					if (ctl is ToolStripMenuItem)
						(ctl as ToolStripMenuItem).ShortcutKeys = kmd.keys;
				}//if
			}
			else if (cmp is Button)
			{
				Button ctl = (Button)cmp;
				kmd = list.get(ctl.Name.after(PrefixButton));
				if (kmd != Kommand.Default)
				{
					ctl.Text = kmd.Caption;
					ctl.Tag = kmd;
					ctl.Click += click;
				}//if
			}
		}//func		


	}//class
}//ns

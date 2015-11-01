using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
	public class Dev1 
	{
		public bool IsOK { get; set; }
		public Dev1()
		{
			IsOK = true;
		}//constructor
	}
	public class Dev2 { 
		public bool IsOnline { get; set; } 
		public static Dev2 Instance;

		public Dev2()
		{
			IsOnline = true;
		}//constructor

		internal void Offline()
		{
			IsOnline = false;
		}
	}


	public partial class smDialog
	{
		partial void checkIsFunc()
		{
			IsCheckOK = true;
		}
		partial void actDoOne()
		{
			IsActOK = true;
		}
	}//class
}//ns

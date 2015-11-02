using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
	public class Dev1 
	{
		public bool IsOnline;
		public bool Online;
		public bool IsOK { get; set; }
		public Dev1()
		{
			IsOK = true;
		}//constructor
	}
	public class Dev2 { 
		public bool IsOnline { get; set; } 
		public static Dev2 Instance;
		public bool IsOK;

		public Dev2()
		{
			IsOnline = true;
		}//constructor

		internal void Offline()
		{
			IsOnline = false;
		}

		internal void Do(int p)
		{
			throw new NotImplementedException();
		}
	}


	public partial class smDialog
	{
	
	}//class
}//ns

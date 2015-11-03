using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
	public class DevOne 
	{
		public bool IsOnline;
		public bool IsOK { get; set; }
		public DevOne()
		{
			IsOK = true;
		}//constructor
	}
	public class DevTwo { 
		public bool IsOnline { get; set; } 
		public static DevTwo Instance;
		public bool IsOK;

		public DevTwo()
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

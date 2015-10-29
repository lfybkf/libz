using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
	public class Dev1 { public bool IsOK { get; set; } }
	public class Dev2 { public bool IsCorrect { get; set; } public static Dev2 Instance;
	internal void Offline()
	{
		throw new NotImplementedException();
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

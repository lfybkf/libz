using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
	public partial class smDialog
	{
		partial void checkIsUserHasRights()
		{
			IsCheckOK = true;
		}

		partial void checkIsAllGood()
		{
			IsCheckOK = true;
		}

		partial void actDoOne()
		{
			IsActOK = true;
		}

		partial void actMake()
		{
			IsActOK = true;
		}
	}//class
}//ns

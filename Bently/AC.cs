using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	public class AC2
	{
		public static AC2 Instance = new AC2();
		public IStoreSQL StoreSQL { get; set; }
		public SimpleLogger log = new SimpleLogger("base{0}");
	}
}

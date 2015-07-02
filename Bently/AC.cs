using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	public class AC
	{
		public static AC Instance = new AC();
		public IStoreSQL StoreSQL { get; set; }
		public SimpleLogger log = new SimpleLogger("base{0}");
	}
}

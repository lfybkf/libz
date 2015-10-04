using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB
{
	[AttributeUsage(AttributeTargets.Class)]
	public class TableAttribute : Attribute
	{
		public string Value { get; set; }
		public TableAttribute(string Value) { this.Value = Value; }
		public string getValue() { return Value; }
	}//class

	[AttributeUsage(AttributeTargets.Property)]
	public class SqlTypeAttribute : Attribute
	{
		public SqlTypes Value { get; set; }
		public SqlTypeAttribute(SqlTypes Value) { this.Value = Value; }
	}//class

	[AttributeUsage(AttributeTargets.Property)]
	public class DefaultAttribute : Attribute
	{
		public string Value { get; set; }
		public DefaultAttribute(string Value) { this.Value = Value; }
	}//class

}//ns

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Enums
{
	public enum ProductStatusEnum
	{
		[Description("InStock")]
		InStock = 1,

		[Description("Deleted")]
		Deleted = 2,

		[Description("OutOfStock")]
		OutOfStock = 3,
	}
}

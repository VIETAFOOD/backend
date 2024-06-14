using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Enums
{
	public enum OrderStatusEnum
	{
		[Description("Unpaid")]
		Unpaid = 1,

		[Description("Paid")]
		Paid = 2,

		[Description("Shipping")]
		Shipping = 3,

		[Description("Delivered")]
		Delivered = 4,

		[Description("Deleted")]
		Deleted = 5,
	}
}

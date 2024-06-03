using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto.Order
{
	public class OrderResponse
	{
		public string OrderKey { get; set; }
		public string CustomerInfoKey { get; set; }
		public string CouponKey { get; set; }
		public DateTime CreatedAt { get; set; }
		public string OrderStatus { get; set; }
		public decimal TotalPrice { get; set; }
		public byte Status { get; set; }
	}
}

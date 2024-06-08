using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Dto.Coupon;
using BusinessObjects.Dto.CustomerInformation;
using BusinessObjects.Dto.OrderDetail;

namespace BusinessObjects.Dto.Order
{
	public class OrderResponse
	{
		public string OrderKey { get; set; }
		public CustomerInformationResponse CustomerInfo { get; set; }
		public List<OrderDetailResponse> OrderDetails { get; set; }
		public CouponResponse? CouponInfo { get; set; } = null;
		public DateTime CreatedAt { get; set; }
		public decimal TotalPrice { get; set; }
		public string Status { get; set; }
	}
}

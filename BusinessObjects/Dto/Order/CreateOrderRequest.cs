using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Dto.CustomerInformation;
using BusinessObjects.Dto.OrderDetail;

namespace BusinessObjects.Dto.Order
{
	public class CreateOrderRequest
	{
		public CreateCustomerInformationRequest CustomerInfo { get; set; }
		public List<CreateOrderDetailRequest> Items { get; set; }
		public string? CouponKey { get; set; }
		public decimal TotalPrice { get; set; }
	}

}

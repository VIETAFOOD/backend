using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Dto.CustomerInformation;
using BusinessObjects.Dto.Order;
using BusinessObjects.Dto.Product;

namespace BusinessObjects.Dto.OrderDetail
{
	public class OrderDetailResponse
	{
		public string OrderDetailKey { get; set; }
		public ProductResponse Product { get; set; }
		public CustomerInformationResponse CustomerInfo { get; set; }
		public OrderResponse Order {  get; set; }
		public int Quantity { get; set; }
		public float ActualPrice { get; set; }
	}
}

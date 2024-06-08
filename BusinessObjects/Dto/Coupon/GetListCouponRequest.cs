using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Request.Paging;

namespace BusinessObjects.Dto.Coupon
{
	public class GetListCouponRequest : PagingRequest
	{
		public string? CouponCode { get; set; }
		public float? DiscountPercentage { get; set; }
	}
}

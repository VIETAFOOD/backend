using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto.Coupon
{
	public class CouponResponse
	{
		public string CouponKey { get; set; } = null!;
		public string? CouponCode { get; set; }
		public string Description { get; set; } = null!;
		public double DiscountPercentage { get; set; }
		public int NumOfUses { get; set; }
		public DateTime ExpiredDate { get; set; }
		public string Email { get; set; } = null!;
		public DateTime CreatedDate { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto.Coupon
{
	public class CreateCouponRequest
	{

        public string Email { get; set; }
        public string? CouponCode { get; set; }
		public string Description { get; set; } = null!;
		public double DiscountPercentage { get; set; }
		public int NumOfUses { get; set; }
		public int ExpiredDate { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto.Coupon
{
	public class UpdateCouponRequest
	{
		public string CouponName { get; set; }
		public float DiscountPercentage { get; set; }
		public int NumOfUses { get; set; }
		public int ExpiredDate { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto.Coupon
{
	public class CouponResponse
	{
		public string CouponKey { get; set; }
		public string CouponName { get; set; }
		public float DiscountPercentage { get; set; }
		public int NumOfUses { get; set; }
		public DateTime ExpiredDate { get; set; }
		public int CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public int Status { get; set; }
	}
}

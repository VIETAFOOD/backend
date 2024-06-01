using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities
{
    public partial class Coupon
    {
        public Coupon()
        {
            Orders = new HashSet<Order>();
        }

        public string CouponKey { get; set; } = null!;
        public string CouponName { get; set; } = null!;
        public double DiscountPercentage { get; set; }
        public int NumOfUses { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

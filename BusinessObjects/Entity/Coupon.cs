using System;
using System.Collections.Generic;

namespace BusinessObjects.Entity
{
    public partial class Coupon
    {
        public Coupon()
        {
            Orders = new HashSet<Order>();
        }

        public int CouponId { get; set; }
        public string CouponName { get; set; } = null!;
        public double DiscountPercentage { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

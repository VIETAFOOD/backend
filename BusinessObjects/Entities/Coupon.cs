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
        public string? CouponCode { get; set; }
        public string Description { get; set; } = null!;
        public double DiscountPercentage { get; set; }
        public int NumOfUses { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public byte Status { get; set; }

        public virtual Admin CreatedByNavigation { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string OrderKey { get; set; } = null!;
        public string? CustomerInfoKey { get; set; }
        public string? CouponKey { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public byte Status { get; set; }

        public virtual Coupon? CouponKeyNavigation { get; set; }
        public virtual CustomerInformation? CustomerInfoKeyNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

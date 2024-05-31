using System;
using System.Collections.Generic;

namespace BusinessObjects.Entity
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int CouponId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string OrderStatus { get; set; } = null!;
        public decimal TotalPrice { get; set; }

        public virtual Coupon Coupon { get; set; } = null!;
        public virtual Account User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BusinessObjects.Entity
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string GuildToUsing { get; set; } = null!;
        public double Weight { get; set; }
        public DateTime CreatedDay { get; set; }
        public int ExpiryDay { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int Quantity { get; set; }
        public byte Status { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

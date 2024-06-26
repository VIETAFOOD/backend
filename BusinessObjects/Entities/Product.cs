﻿using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string ProductKey { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string GuildToUsing { get; set; } = null!;
        public string Weight { get; set; } = null!;
        public string ExpiryDay { get; set; } = null!;
        public double Price { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int Quantity { get; set; }
        public byte Status { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

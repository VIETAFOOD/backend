using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities
{
    public partial class OrderDetail
    {
        public string OrderDetailKey { get; set; } = null!;
        public string? ProductKey { get; set; }
        public string? OrderKey { get; set; }
        public int? Quantity { get; set; }
        public double ActualPrice { get; set; }

        public virtual Order? OrderKeyNavigation { get; set; }
        public virtual Product? ProductKeyNavigation { get; set; }
    }
}

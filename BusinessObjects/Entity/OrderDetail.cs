using System;
using System.Collections.Generic;

namespace BusinessObjects.Entity
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? CustomerInformationId { get; set; }
        public int? ProductId { get; set; }
        public int? OrderId { get; set; }
        public double ActualPrice { get; set; }
        public byte Status { get; set; }

        public virtual CustomerInformation? CustomerInformation { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}

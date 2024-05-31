using System;
using System.Collections.Generic;

namespace BusinessObjects.Entity
{
    public partial class CustomerInformation
    {
        public CustomerInformation()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int CustomerInformationId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities
{
    public partial class CustomerInformation
    {
        public CustomerInformation()
        {
            Orders = new HashSet<Order>();
        }

        public string CustomerInfoKey { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}

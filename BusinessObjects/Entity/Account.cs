using System;
using System.Collections.Generic;

namespace BusinessObjects.Entity
{
    public partial class Account
    {
        public Account()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public byte IsAdmin { get; set; }
        public byte IsBlocked { get; set; }
        public string Gender { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}

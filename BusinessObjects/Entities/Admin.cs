using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities
{
    public partial class Admin
    {
        public string AdminKey { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto.Admin
{
    public class LoginResponse
    {
        public string email {  get; set; }
        public string token { get; set; }
    }
}

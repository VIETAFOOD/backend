﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto.CustomerInformation
{
	public class UpdateCustomerInformationRequest
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
	}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto.Order
{
	public class UpdateOrderRequest
	{
		public string CustomerInfoKey { get; set; }
		public byte Status { get; set; }
	}
}

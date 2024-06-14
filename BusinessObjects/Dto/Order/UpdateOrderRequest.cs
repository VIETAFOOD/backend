using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto.Order
{
	public class UpdateOrderRequest
	{
		public string CustomerInfoKey { get; set; }
		[Range(1, 4, ErrorMessage = "Status must be between 1 and 4.")]
		public byte Status { get; set; }
	}
}

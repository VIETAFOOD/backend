﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto.Product
{
	public class ProductResponse
	{
		public string ProductKey { get; set; } = null!;
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string GuildToUsing { get; set; } = null!;
		public double Weight { get; set; }
		public DateTime CreatedDay { get; set; }
		public int ExpiryDay { get; set; }
		public string ImageUrl { get; set; } = null!;
		public int Quantity { get; set; }
		public byte Status { get; set; }
	}
}
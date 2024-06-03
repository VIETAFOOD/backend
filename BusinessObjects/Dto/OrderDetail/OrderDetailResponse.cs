﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto.OrderDetail
{
	public class OrderDetailResponse
	{
		public string OrderDetailKey { get; set; }
		public string ProductKey { get; set; }
		public string OrderKey { get; set; }
		public int Quantity { get; set; }
		public float ActualPrice { get; set; }
	}
}

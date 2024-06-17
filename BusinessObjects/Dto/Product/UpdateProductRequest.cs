using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto.Product
{
	public class UpdateProductRequest
	{
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string GuildToUsing { get; set; } = null!;
        public string Weight { get; set; } = null!;
        public double Price { get; set; }
        public string ExpiryDay { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public int Quantity { get; set; }
    }
}

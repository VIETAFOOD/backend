using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Request.Paging;

namespace BusinessObjects.Dto.Product
{
	public class GetListProductRequest : PagingRequest
	{
		public string? Name { get; set; }
		public string? SortOption { get; set; }
		public bool isSortDesc { get; set; } = false;
	}
}

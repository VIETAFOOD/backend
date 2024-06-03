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
	}
}

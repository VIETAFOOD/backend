﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extentions.Paginate
{
    public interface IPaginate<TResult>
    {
		IList<TResult> Items { get; set; }
		int TotalCount { get; set; }
		int PageSize { get; set; }
		int PageIndex { get; set; }
		int TotalPages { get; set; }
	}

    public class PaginatedList<TResult> : IPaginate<TResult>
    {
		public IList<TResult> Items { get; set; }
		public int TotalCount { get; set; }
		public int PageSize { get; set; }
		public int PageIndex { get; set; }
        public int TotalPages { get; set; }

		public PaginatedList(IEnumerable<TResult> source, int page, int size, int firstPage)
        {
            var enumerable = source as TResult[] ?? source.ToArray();

            if (firstPage > page)
            {
                throw new ArgumentException($"Page ({page}) must be greater or equal than firstPage ({firstPage})");
            }

            if (source is IQueryable<TResult> queryable)
            {
				PageIndex = page;
				PageSize = size;
				TotalCount = queryable.Count();
                Items = queryable.Skip((page - firstPage) * size).Take(size).ToList();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            }
            else
            {
				PageIndex = page;
				PageSize = size;
				TotalCount = enumerable.Length;
                Items = enumerable.Skip((page - firstPage) * size).Take(size).ToList();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            }
        }

        public PaginatedList()
        {
            Items = Array.Empty<TResult>();
        }
    }
}

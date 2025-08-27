using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Pagination
{
    public class QueryParameters
    {
        const int MaxPageSize = 50;
        public int CurrentPage { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }
        public string SortField { get; set; } = string.Empty;
        public string SortOrder { get; set; } = "asc";
        public string? SearchValue { get; set; } = string.Empty;

        public class PagedList<T>
        {
            public int CurrentPage { get; private set; }
            public int TotalPages { get; private set; }
            public int PageSize { get; private set; }
            public int TotalCount { get; private set; }
            public bool HasPrevious => CurrentPage > 1;
            public bool HasNext => CurrentPage < TotalPages;
            public List<T> Data { get; set; }

            public PagedList(List<T> items, int count, int pageNumber, int pageSize)
            {
                TotalCount = count;
                PageSize = pageSize;
                CurrentPage = pageNumber;
                TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                Data = (items);
            }
            public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
            {
                 var count = source.Count();
                var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return new PagedList<T>(items, count, pageNumber, pageSize);
            }


        }
    }

}

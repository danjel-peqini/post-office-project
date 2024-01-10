using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Pagination
{
    public class Pagination<T> where T : class
    {
        public List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentPageSize { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public long TotalElements { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public Pagination(List<T> items, long count, int pageIndex, int pageSize, bool skipData = false)
        {
            Data = !skipData ? items : items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            TotalElements = count;
            PageSize = pageSize;
            CurrentPage = pageIndex;
            CurrentPageSize = Data.Count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
        public static Pagination<T> ToPagedList(List<T> source, int count, int pageNumber, int pageSize, bool skipData = true)
        {
            return new Pagination<T>(source, count, pageNumber, pageSize, skipData);
        }

        public static Pagination<T> ToPagedList<E>(QueryParameters.PagedList<E> entityPage, Func<List<E>, List<T>> mapFunction)
        {
            return new Pagination<T>(mapFunction.Invoke(entityPage.Data), entityPage.TotalCount, entityPage.CurrentPage, entityPage.PageSize);

        }
    }
}

using DAL.Contracts;
using Entities.Models;
using Helpers.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Concrete
{
    internal class PostOfficeRepository : BaseRepository<TblPostOffice, Guid>, IPostOfficeRepository
    {
        public PostOfficeRepository(PostOfficeDBContext dbContext) : base(dbContext)
        {
        }

        public TblPostOffice CheckUniquePostOfficeName(string name)
        {
            return context.FirstOrDefault(x => x.Name == name && x.IsActive.Value == true);
        }

        public PagedList<TblPostOffice> GetAll(QueryParameters queryParameters, string? searchValue)
        {
            var data = context.Where(x => x.IsActive == true);
            if (!String.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x => x.Name.Contains(searchValue));
            }
            var filterData = PaginationConfiguration(data, queryParameters.SortField, queryParameters.SortOrder, "");
            return PagedList<TblPostOffice>.ToPagedList(filterData, queryParameters == null ? 1 : queryParameters.CurrentPage, queryParameters == null ? 10 : queryParameters.PageSize);
        }
        public TblPostOffice GetById(Guid postOfficeId)
        {
            return context.Where(x => x.PostOfficeId == postOfficeId && x.IsActive.Value).AsNoTracking().FirstOrDefault();
        }
    }
}

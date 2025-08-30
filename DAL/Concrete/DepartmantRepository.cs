using DAL.Contracts;
using Entities.Models;
using Helpers;
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
    internal class DepartmantRepository : BaseRepository<TblDepartment, Guid>, IDepartmantRepository
    {
        public DepartmantRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }
        public PagedList<TblDepartment> GetDeparttmants(QueryParameters queryParameters)
        {
            var data = context.Where(x => x.Status != EntityStatus.Deleted);
            var filterData = PaginationConfiguration(data, queryParameters.SortField, queryParameters.SortOrder, queryParameters.SearchValue);
            return PagedList<TblDepartment>.ToPagedList(filterData, queryParameters == null ? 1 : queryParameters.CurrentPage, queryParameters == null ? 10 : queryParameters.PageSize);

        }
    }
}

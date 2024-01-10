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
    internal class ShipmentRepository : BaseRepository<TblShipment, Guid>, IShipmentRepository
    {
        public ShipmentRepository(PostOfficeDBContext dbContext) : base(dbContext)
        {
        }

        public PagedList<TblShipment> GetAll(QueryParameters queryParameters, string? searchValue, Guid userId)
        {
            var data = context.Where(x => x.CreatedBy == userId);
            var filterData = PaginationConfiguration(data, queryParameters.SortField, queryParameters.SortOrder, "");
            return PagedList<TblShipment>.ToPagedList(filterData, queryParameters == null ? 1 : queryParameters.CurrentPage, queryParameters == null ? 10 : queryParameters.PageSize);
        }
    }
}

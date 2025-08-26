using DAL.Contracts;
using Entities.Models;
using Helpers.Pagination;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Concrete
{
    internal class RoomRepository : BaseRepository<TblRoom, Guid>, IRoomRepository
    {
        public RoomRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }

        public PagedList<TblRoom> GetRooms(QueryParameters queryParameters)
        {
            var data = context;
            var filterData = PaginationConfiguration(data, queryParameters.SortField, queryParameters.SortOrder, queryParameters.SearchValue);
            return PagedList<TblRoom>.ToPagedList(filterData, queryParameters == null ? 1 : queryParameters.CurrentPage, queryParameters == null ? 10 : queryParameters.PageSize);
        }
    }
}

using DAL.Contracts;
using Entities.Models;
using Helpers.Pagination;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Concrete
{
    internal class ScheduleRepository : BaseRepository<TblSchedule, Guid>, IScheduleRepository
    {
        public ScheduleRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }

        public PagedList<TblSchedule> GetSchedules(QueryParameters queryParameters)
        {
            var data = context;
            var filterData = PaginationConfiguration(data, queryParameters.SortField, queryParameters.SortOrder, queryParameters.SearchValue);
            return PagedList<TblSchedule>.ToPagedList(filterData, queryParameters == null ? 1 : queryParameters.CurrentPage, queryParameters == null ? 10 : queryParameters.PageSize);
        }
    }
}

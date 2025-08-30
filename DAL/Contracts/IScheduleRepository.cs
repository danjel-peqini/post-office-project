using Entities.Models;
using Helpers.Pagination;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Contracts
{
    public interface IScheduleRepository : IRepository<TblSchedule, Guid>
    {
        PagedList<TblSchedule> GetSchedules(QueryParameters queryParameters);
    }
}

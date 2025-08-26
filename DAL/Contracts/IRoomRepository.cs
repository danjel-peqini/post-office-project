using Entities.Models;
using Helpers.Pagination;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Contracts
{
    public interface IRoomRepository : IRepository<TblRoom, Guid>
    {
        PagedList<TblRoom> GetRooms(QueryParameters queryParameters);
    }
}

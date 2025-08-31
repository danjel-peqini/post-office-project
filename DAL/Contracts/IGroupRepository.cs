using Entities.Models;
using Helpers.Pagination;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Contracts
{
    public interface IGroupRepository : IRepository<TblGroup, Guid>
    {
        PagedList<TblGroup> GetGroups(QueryParameters queryParameters, Guid? studentId = null);
    }
}

using Entities.Models;
using Helpers.Pagination;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Contracts
{
    public interface IProgramRepository : IRepository<TblProgram, Guid>
    {
        PagedList<TblProgram> GetPrograms(QueryParameters queryParameters);
    }
}

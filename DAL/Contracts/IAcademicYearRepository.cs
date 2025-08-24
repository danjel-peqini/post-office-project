using Entities.Models;
using Helpers.Pagination;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Contracts
{
    public interface IAcademicYearRepository : IRepository<TblAcademicYear, Guid>
    {
        PagedList<TblAcademicYear> GetAcademicYears(QueryParameters queryParameters);
    }
}

using DAL.Contracts;
using Entities.Models;
using Helpers;
using Helpers.Pagination;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Concrete
{
    internal class AcademicYearRepository : BaseRepository<TblAcademicYear, Guid>, IAcademicYearRepository
    {
        public AcademicYearRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }

        public PagedList<TblAcademicYear> GetAcademicYears(QueryParameters queryParameters)
        {
            var data = context.Where(x => x.Status != EntityStatus.Deleted);
            var filtered = PaginationConfiguration(data, queryParameters.SortField, queryParameters.SortOrder, queryParameters.SearchValue);
            return PagedList<TblAcademicYear>.ToPagedList(filtered, queryParameters?.CurrentPage ?? 1, queryParameters?.PageSize ?? 10);
        }
    }
}

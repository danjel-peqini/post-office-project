using DAL.Contracts;
using Entities.Models;
using Helpers;
using Helpers.Pagination;
using Microsoft.EntityFrameworkCore;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Concrete
{
    internal class ProgramRepository : BaseRepository<TblProgram, Guid>, IProgramRepository
    {
        public ProgramRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }

        public PagedList<TblProgram> GetPrograms(QueryParameters queryParameters)
        {
            var data = context
                .Include(p => p.Department)
                .Where(p => p.Status != EntityStatus.Deleted);
            var filterData = PaginationConfiguration(data, queryParameters.SortField, queryParameters.SortOrder, queryParameters.SearchValue);
            return PagedList<TblProgram>.ToPagedList(filterData, queryParameters == null ? 1 : queryParameters.CurrentPage, queryParameters == null ? 10 : queryParameters.PageSize);
        }

        public override TblProgram GetById(Guid id)
        {
            return context
                .Include(p => p.Department)
                .FirstOrDefault(p => p.Id == id && p.Status != EntityStatus.Deleted);
        }
    }
}

using DAL.Contracts;
using Entities.Models;
using Helpers;
using Helpers.Pagination;
using Microsoft.EntityFrameworkCore;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Concrete
{
    internal class CourseRepository : BaseRepository<TblCourse, Guid>, ICourseRepository
    {
        public CourseRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }

        public PagedList<TblCourse> GetCourses(QueryParameters queryParameters)
        {
            var data = context
                .Include(x => x.Program)
                    .ThenInclude(p => p.Department)
                .Where(x => x.Status != EntityStatus.Deleted);
            var filterData = PaginationConfiguration(data, queryParameters.SortField, queryParameters.SortOrder, queryParameters.SearchValue);
            return PagedList<TblCourse>.ToPagedList(filterData, queryParameters == null ? 1 : queryParameters.CurrentPage, queryParameters == null ? 10 : queryParameters.PageSize);
        }

        public override TblCourse GetById(Guid id)
        {
            return context
                .Include(x => x.Program)
                    .ThenInclude(p => p.Department)
                .FirstOrDefault(x => x.Id == id && x.Status != EntityStatus.Deleted);
        }
    }
}


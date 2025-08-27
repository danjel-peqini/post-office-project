using DAL.Contracts;
using Entities.Models;
using Helpers.Pagination;
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
            var data = context.Where(x => x.IsActive);
            var filterData = PaginationConfiguration(data, queryParameters.SortField, queryParameters.SortOrder, queryParameters.SearchValue);
            return PagedList<TblCourse>.ToPagedList(filterData, queryParameters == null ? 1 : queryParameters.CurrentPage, queryParameters == null ? 10 : queryParameters.PageSize);
        }
    }
}


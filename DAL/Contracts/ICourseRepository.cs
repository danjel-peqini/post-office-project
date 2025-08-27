using Entities.Models;
using Helpers.Pagination;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Contracts
{
    public interface ICourseRepository : IRepository<TblCourse, Guid>
    {
        PagedList<TblCourse> GetCourses(QueryParameters queryParameters);
    }
}


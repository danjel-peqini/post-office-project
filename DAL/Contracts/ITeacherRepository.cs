using Entities.Models;
using Helpers.Pagination;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Contracts
{
    public interface ITeacherRepository : IRepository<TblTeacher, Guid>
    {
        PagedList<TblTeacher> GetTeachers(QueryParameters queryParameters);
        TblTeacher GetByUserId(Guid userId);
    }
}

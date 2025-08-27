using DTO;
using Helpers.Pagination;

namespace Domain.Contracts
{
    public interface ICourseDomain
    {
        Pagination<CourseDTO> GetAllCourses(QueryParameters queryParameters);
        CourseDTO GetCourseById(Guid id);
        void AddNew(CoursePostDTO course);
        CourseDTO Update(Guid id, CoursePostDTO course);
        void Delete(Guid id);
    }
}


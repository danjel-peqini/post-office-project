using DTO;
using Helpers.Pagination;

namespace Domain.Contracts
{
    public interface ITeacherDomain
    {
        Pagination<TeacherDTO> GetAllTeachers(QueryParameters queryParameters);
        TeacherDTO GetTeacherById(Guid id);
        TeacherDTO GetTeacherByUserId(Guid userId);
        TeacherDTO AddNew(TeacherPostDTO teacherPostDTO);
        TeacherDTO Update(Guid id, TeacherPostDTO teacherPostDTO);
        void Delete(Guid id);
        void DeleteByUserId(Guid userId);
    }
}

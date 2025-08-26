using DTO;
using DTO.UserDTO;
using DTO.UserTypeDTO;
using Helpers.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IDepartmantDomain
    {
        Pagination<DepartmantDTO> GetAllDepartmants(QueryParameters queryParameters);
        DepartmantDTO GetDepartmantBtyd(Guid id);
        void AddNew(DepartmantPostDTO departmantPostDTO);
        void delete(Guid id);
        CourseDTO addCourse(CoursePostDTO course, Guid departmantId);
        CourseDTO updateCourse(CoursePostDTO course, Guid courseId);


    }
}

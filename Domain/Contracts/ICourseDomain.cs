using DTO;
using System;

namespace Domain.Contracts
{
    public interface ICourseDomain
    {
        CourseDTO Create(CourseCreateDTO dto);
    }
}

using System;
using System.Collections.Generic;
using Helpers;

namespace DTO
{
    public class GroupDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CourseId { get; set; }
        public Guid AcademicYearId { get; set; }
        public CourseDTO Course { get; set; }
        public AcademicYearDTO AcademicYear { get; set; }
        public List<Guid> StudentIds { get; set; }
        public List<StudentCardDTO> Students { get; set; }
        public int StudentsLength { get; set; }
        public EntityStatus Status { get; set; }
    }

    public class GroupPostDTO
    {
        public string? Name { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? AcademicYearId { get; set; }
        public List<Guid>? StudentIds { get; set; }
        public EntityStatus? Status { get; set; }
    }
}

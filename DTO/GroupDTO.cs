using System;

namespace DTO
{
    public class GroupDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CourseId { get; set; }
        public Guid AcademicYearId { get; set; }
    }

    public class GroupPostDTO
    {
        public string? Name { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? AcademicYearId { get; set; }
    }
}

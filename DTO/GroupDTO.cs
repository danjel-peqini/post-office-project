using System;
using System.Collections.Generic;

namespace DTO
{
    public class GroupDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid CourseId { get; set; }
        public Guid AcademicYearId { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace DTO
{
    public class CourseCreateDTO
    {
        public string Name { get; set; } = null!;
        public Guid DepartmentId { get; set; }
        public int Credits { get; set; }
        public int? TotalHours { get; set; }
        public Guid AcademicYearId { get; set; }
        public string GroupName { get; set; } = null!;
        public IEnumerable<Guid> StudentIds { get; set; } = new List<Guid>();
        public IEnumerable<SchedulePostDTO> Schedules { get; set; } = new List<SchedulePostDTO>();
    }
}

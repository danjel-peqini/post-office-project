using System;
using Helpers;

namespace DTO
{
    public class ScheduleDTO
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public Guid CourseId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid RoomId { get; set; }
        public Guid AcademicYearId { get; set; }
        public Helpers.DayOfWeek DayOfWeek { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public RoomType ScheduleType { get; set; }

        public GroupDTO Group { get; set; }
        public CourseDTO Course { get; set; }
        public TeacherDTO Teacher { get; set; }
        public RoomDTO Room { get; set; }
        public AcademicYearDTO AcademicYear { get; set; }
    }

    public class SchedulePostDTO
    {
        public Guid? GroupId { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? TeacherId { get; set; }
        public Guid? RoomId { get; set; }
        public Guid? AcademicYearId { get; set; }
        public Helpers.DayOfWeek? DayOfWeek { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public RoomType? ScheduleType { get; set; }
    }
}

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
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public RoomType ScheduleType { get; set; }
    }

    public class SchedulePostDTO
    {
        public Guid? GroupId { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? TeacherId { get; set; }
        public Guid? RoomId { get; set; }
        public Guid? AcademicYearId { get; set; }
        public Helpers.DayOfWeek? DayOfWeek { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public RoomType? ScheduleType { get; set; }
    }
}

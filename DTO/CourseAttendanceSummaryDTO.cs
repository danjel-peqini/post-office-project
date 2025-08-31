using System;

namespace DTO
{
    public class CourseAttendanceSummaryDTO
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public int TotalSessions { get; set; }
        public int AttendedSessions { get; set; }
        public int MissedSessions { get; set; }
    }
}

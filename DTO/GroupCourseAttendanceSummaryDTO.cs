using System;

namespace DTO
{
    public class GroupCourseAttendanceSummaryDTO
    {
        public Guid StudentId { get; set; }
        public string StudentCardCode { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int AttendedSessions { get; set; }
        public int TotalSessions { get; set; }
        public int MissedSessions { get; set; }
        public int TotalPlannedSessions { get; set; }
    }
}

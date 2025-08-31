using System;

namespace DTO
{
    public class GroupSessionAttendanceDTO
    {
        public Guid StudentId { get; set; }
        public string StudentCardCode { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool HasAttended { get; set; }
        public DateTimeOffset? CheckInTime { get; set; }
    }
}


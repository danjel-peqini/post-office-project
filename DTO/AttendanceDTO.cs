using System;
using Helpers;

namespace DTO
{
    public class AttendanceDTO
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public Guid StudentId { get; set; }
        public DateTimeOffset CheckInTime { get; set; }
        public string CheckedInBy { get; set; } = null!;
        public EntityStatus Status { get; set; }
    }

    public class AttendanceCheckInDTO
    {
        public string StudentCardCode { get; set; } = null!;
        public Guid SessionId { get; set; }
    }

    public class AttendanceAddDTO
    {
        public Guid SessionId { get; set; }
        public Guid StudentId { get; set; }
    }
}

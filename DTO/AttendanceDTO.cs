using System;

namespace DTO
{
    public class AttendanceDTO
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public Guid StudentId { get; set; }
        public DateTime CheckInTime { get; set; }
        public string CheckedInBy { get; set; } = null!;
    }

    public class AttendanceCheckInDTO
    {
        public string StudentCardCode { get; set; } = null!;
        public Guid SessionId { get; set; }
    }
}

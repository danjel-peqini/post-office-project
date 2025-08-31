using System;
using Helpers;

namespace DTO
{
    public class SessionDTO
    {
        public Guid Id { get; set; }
        public Guid ScheduleId { get; set; }
        public DateTime Date { get; set; }
        public bool? IsOpen { get; set; }
        public string? Otp { get; set; }
        public DateTime? OtpcreatedAt { get; set; }
        public EntityStatus Status { get; set; }
        public string? IpAddress { get; set; }

        public ScheduleDTO Schedule { get; set; }
    }
}

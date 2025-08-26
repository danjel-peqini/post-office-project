using System;

namespace DTO
{
    public class ScheduleDTO
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Guid RoomId { get; set; }
        public int DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

    public class SchedulePostDTO
    {
        public Guid TeacherId { get; set; }
        public Guid RoomId { get; set; }
        public int DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}

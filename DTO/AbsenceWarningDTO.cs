using System;
using Helpers;

namespace DTO
{
    public class AbsenceWarningDTO
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public double Percentage { get; set; }
        public DateTime SentAt { get; set; }
        public EntityStatus Status { get; set; }
    }
}

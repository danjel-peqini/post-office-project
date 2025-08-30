using System;
using System.Collections.Generic;
using Helpers;

namespace Entities.Models
{
    public partial class TblAbsenceWarning
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public double Percentage { get; set; }
        public DateTime SentAt { get; set; }
        public EntityStatus Status { get; set; }

        public virtual TblCourse Course { get; set; } = null!;
        public virtual TblStudentCard Student { get; set; } = null!;
    }
}

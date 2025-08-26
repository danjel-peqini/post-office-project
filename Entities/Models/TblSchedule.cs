using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class TblSchedule
    {
        public TblSchedule()
        {
            TblSessions = new HashSet<TblSession>();
        }

        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public Guid CourseId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid RoomId { get; set; }
        public Guid AcademicYearId { get; set; }
        public int DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public virtual TblCourse Course { get; set; } = null!;
        public virtual TblGroup Group { get; set; } = null!;
        public virtual TblAcademicYear AcademicYear { get; set; } = null!;
        public virtual TblRoom Room { get; set; } = null!;
        public virtual TblTeacher Teacher { get; set; } = null!;
        public virtual ICollection<TblSession> TblSessions { get; set; }
    }
}

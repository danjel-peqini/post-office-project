using System;
using System.Collections.Generic;
using Helpers;

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
        public Guid ProgramId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid RoomId { get; set; }
        public Guid AcademicYearId { get; set; }
        public Helpers.DayOfWeek DayOfWeek { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public RoomType ScheduleType { get; set; }
        public EntityStatus Status { get; set; }

        public virtual TblProgram Program { get; set; } = null!;
        public virtual TblGroup Group { get; set; } = null!;
        public virtual TblAcademicYear AcademicYear { get; set; } = null!;
        public virtual TblRoom Room { get; set; } = null!;
        public virtual TblTeacher Teacher { get; set; } = null!;
        public virtual ICollection<TblSession> TblSessions { get; set; }
    }
}

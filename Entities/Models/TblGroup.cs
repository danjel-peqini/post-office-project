using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class TblGroup
    {
        public TblGroup()
        {
            TblGroupStudents = new HashSet<TblGroupStudent>();
            TblSchedules = new HashSet<TblSchedule>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid CourseId { get; set; }
        public Guid AcademicYearId { get; set; }

        public virtual TblAcademicYear AcademicYear { get; set; } = null!;
        public virtual TblCourse Course { get; set; } = null!;
        public virtual ICollection<TblGroupStudent> TblGroupStudents { get; set; }
        public virtual ICollection<TblSchedule> TblSchedules { get; set; }
    }
}

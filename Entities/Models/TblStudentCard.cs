using System;
using System.Collections.Generic;
using Helpers;

namespace Entities.Models
{
    public partial class TblStudentCard
    {
        public TblStudentCard()
        {
            TblAbsenceWarnings = new HashSet<TblAbsenceWarning>();
            TblAttendances = new HashSet<TblAttendance>();
            TblGroupStudents = new HashSet<TblGroupStudent>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid AcademicYearId { get; set; }
        public string StudentCardCode { get; set; } = null!;
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? DisabledDate { get; set; }
        public EntityStatus Status { get; set; }

        public virtual TblAcademicYear AcademicYear { get; set; } = null!;
        public virtual TblDepartment Department { get; set; } = null!;
        public virtual TblUser User { get; set; } = null!;
        public virtual ICollection<TblAbsenceWarning> TblAbsenceWarnings { get; set; }
        public virtual ICollection<TblAttendance> TblAttendances { get; set; }
        public virtual ICollection<TblGroupStudent> TblGroupStudents { get; set; }
    }
}

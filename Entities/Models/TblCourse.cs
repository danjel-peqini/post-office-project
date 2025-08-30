using System;
using System.Collections.Generic;
using Helpers;

namespace Entities.Models
{
    public partial class TblCourse
    {
        public TblCourse()
        {
            TblAbsenceWarnings = new HashSet<TblAbsenceWarning>();
            TblGroups = new HashSet<TblGroup>();
            TblSchedules = new HashSet<TblSchedule>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid DepartmentId { get; set; }
        public EntityStatus Status { get; set; }
        public int Credits { get; set; }
        public int? TotalHours { get; set; }

        public virtual TblDepartment Department { get; set; } = null!;
        public virtual ICollection<TblAbsenceWarning> TblAbsenceWarnings { get; set; }
        public virtual ICollection<TblGroup> TblGroups { get; set; }
        public virtual ICollection<TblSchedule> TblSchedules { get; set; }
    }
}

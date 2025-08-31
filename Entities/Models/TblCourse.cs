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
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid ProgramId { get; set; }
        public EntityStatus Status { get; set; }
        public int Credits { get; set; }
        public int? TotalHours { get; set; }

        public virtual TblProgram Program { get; set; } = null!;
        public virtual ICollection<TblAbsenceWarning> TblAbsenceWarnings { get; set; }
    }
}

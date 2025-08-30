using System;
using System.Collections.Generic;
using Helpers;

namespace Entities.Models
{
    public partial class TblAcademicYear
    {
        public TblAcademicYear()
        {
            TblGroups = new HashSet<TblGroup>();
            TblStudentCards = new HashSet<TblStudentCard>();
            TblSchedules = new HashSet<TblSchedule>();
        }

        public Guid Id { get; set; }
        public int StartingYear { get; set; }
        public string Year { get; set; } = null!;
        public EntityStatus Status { get; set; }

        public virtual ICollection<TblGroup> TblGroups { get; set; }
        public virtual ICollection<TblStudentCard> TblStudentCards { get; set; }
        public virtual ICollection<TblSchedule> TblSchedules { get; set; }
    }
}

using System;
using System.Collections.Generic;
using Helpers;

namespace Entities.Models
{
    public partial class TblProgram
    {
        public TblProgram()
        {
            TblCourses = new HashSet<TblCourse>();
            TblGroups = new HashSet<TblGroup>();
            TblStudentCards = new HashSet<TblStudentCard>();
            TblSchedules = new HashSet<TblSchedule>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid DepartmentId { get; set; }
        public EntityStatus Status { get; set; }

        public virtual TblDepartment Department { get; set; } = null!;
        public virtual ICollection<TblCourse> TblCourses { get; set; }
        public virtual ICollection<TblGroup> TblGroups { get; set; }
        public virtual ICollection<TblStudentCard> TblStudentCards { get; set; }
        public virtual ICollection<TblSchedule> TblSchedules { get; set; }
    }
}

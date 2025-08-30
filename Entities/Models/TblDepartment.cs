using System;
using System.Collections.Generic;
using Helpers;

namespace Entities.Models
{
    public partial class TblDepartment
    {
        public TblDepartment()
        {
            TblCourses = new HashSet<TblCourse>();
            TblStudentCards = new HashSet<TblStudentCard>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Abbreviation { get; set; } = null!;
        public string? Description { get; set; } = null;
        public DateTimeOffset? CreatedDate { get; set; }
        public EntityStatus Status { get; set; }

        public virtual ICollection<TblCourse> TblCourses { get; set; }
        public virtual ICollection<TblStudentCard> TblStudentCards { get; set; }
    }
}

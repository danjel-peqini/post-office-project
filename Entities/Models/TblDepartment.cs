using System;
using System.Collections.Generic;

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
        public bool IsActive { get; set; }

        public virtual ICollection<TblCourse> TblCourses { get; set; }
        public virtual ICollection<TblStudentCard> TblStudentCards { get; set; }
    }
}

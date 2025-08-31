using System;
using System.Collections.Generic;
using Helpers;

namespace Entities.Models
{
    public partial class TblDepartment
    {
        public TblDepartment()
        {
            TblPrograms = new HashSet<TblProgram>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Abbreviation { get; set; } = null!;
        public string? Description { get; set; } = null;
        public DateTimeOffset? CreatedDate { get; set; }
        public EntityStatus Status { get; set; }

        public virtual ICollection<TblProgram> TblPrograms { get; set; }
    }
}

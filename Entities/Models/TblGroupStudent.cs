using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class TblGroupStudent
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public Guid StudentId { get; set; }

        public virtual TblGroup Group { get; set; } = null!;
        public virtual TblStudentCard Student { get; set; } = null!;
    }
}

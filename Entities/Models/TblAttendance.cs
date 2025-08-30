using System;
using System.Collections.Generic;
using Helpers;

namespace Entities.Models
{
    public partial class TblAttendance
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public Guid StudentId { get; set; }
        public DateTimeOffset CheckInTime { get; set; }
        public string CheckedInBy { get; set; } = null!;
        public EntityStatus Status { get; set; }

        public virtual TblSession Session { get; set; } = null!;
        public virtual TblStudentCard Student { get; set; } = null!;
    }
}

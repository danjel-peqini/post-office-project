using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class TblAttendance
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public Guid StudentId { get; set; }
        public DateTimeOffset CheckInTime { get; set; }
        public string CheckedInBy { get; set; } = null!;

        public virtual TblSession Session { get; set; } = null!;
        public virtual TblStudentCard Student { get; set; } = null!;
    }
}

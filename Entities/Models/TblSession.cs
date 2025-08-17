using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class TblSession
    {
        public TblSession()
        {
            TblAttendances = new HashSet<TblAttendance>();
        }

        public Guid Id { get; set; }
        public Guid ScheduleId { get; set; }
        public DateTime Date { get; set; }
        public bool? IsOpen { get; set; }
        public string? Otp { get; set; }
        public DateTime? OtpcreatedAt { get; set; }

        public virtual TblSchedule Schedule { get; set; } = null!;
        public virtual ICollection<TblAttendance> TblAttendances { get; set; }
    }
}

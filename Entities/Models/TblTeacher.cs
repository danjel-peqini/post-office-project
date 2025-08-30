using System;
using System.Collections.Generic;
using Helpers;

namespace Entities.Models
{
    public partial class TblTeacher
    {
        public TblTeacher()
        {
            TblSchedules = new HashSet<TblSchedule>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public EntityStatus Status { get; set; }

        public virtual TblUser User { get; set; } = null!;
        public virtual ICollection<TblSchedule> TblSchedules { get; set; }
    }
}

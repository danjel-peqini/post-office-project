using System;
using System.Collections.Generic;
using Helpers;

namespace Entities.Models
{
    public partial class TblRoom
    {
        public TblRoom()
        {
            TblSchedules = new HashSet<TblSchedule>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public RoomType RoomType { get; set; }
        public int? SeatsNumber { get; set; }
        public EntityStatus Status { get; set; }

        public virtual ICollection<TblSchedule> TblSchedules { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class TblHistory
    {
        public Guid HistoryId { get; set; }
        public int? ActionId { get; set; }
        public string? Description { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}

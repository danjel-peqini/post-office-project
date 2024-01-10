using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class TblTransportRoute
    {
        public TblTransportRoute()
        {
            TblTransportations = new HashSet<TblTransportation>();
        }

        public Guid RouteId { get; set; }
        public Guid? SourcePostOfficeId { get; set; }
        public Guid? DestinationPostOfficeId { get; set; }

        public virtual TblPostOffice? DestinationPostOffice { get; set; }
        public virtual TblPostOffice? SourcePostOffice { get; set; }
        public virtual ICollection<TblTransportation> TblTransportations { get; set; }
    }
}

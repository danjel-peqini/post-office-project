using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class TblPackage
    {
        public TblPackage()
        {
            TblShipments = new HashSet<TblShipment>();
            TblTransportations = new HashSet<TblTransportation>();
        }

        public Guid PackageId { get; set; }
        public int? Status { get; set; }
        public string? Barcode { get; set; }
        public Guid? UserCreatedId { get; set; }
        public Guid? DestinationPostOfficeId { get; set; }
        public Guid? SourcePostOfficeId { get; set; }
        public DateTimeOffset? DateCreated { get; set; }

        public virtual TblPostOffice? DestinationPostOffice { get; set; }
        public virtual TblPostOffice? SourcePostOffice { get; set; }
        public virtual TblUser? UserCreated { get; set; }
        public virtual ICollection<TblShipment> TblShipments { get; set; }
        public virtual ICollection<TblTransportation> TblTransportations { get; set; }
    }
}

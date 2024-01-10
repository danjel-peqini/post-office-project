using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class TblShipment
    {
        public TblShipment()
        {
            TblTransportations = new HashSet<TblTransportation>();
        }

        public Guid ShipmentId { get; set; }
        public Guid? SendUserId { get; set; }
        public string? ReceiverUserDetails { get; set; }
        public int? Status { get; set; }
        public DateTimeOffset? CreationDate { get; set; }
        public DateTimeOffset? DeliveryDate { get; set; }
        public string? Barcode { get; set; }
        public double? Price { get; set; }
        public Guid? CreatedBy { get; set; }
        public long? TrackNumber { get; set; }
        public string? DeliverPlace { get; set; }
        public Guid? PackageId { get; set; }

        public virtual TblUser? CreatedByNavigation { get; set; }
        public virtual TblPackage? Package { get; set; }
        public virtual TblUser? SendUser { get; set; }
        public virtual ICollection<TblTransportation> TblTransportations { get; set; }
    }
}

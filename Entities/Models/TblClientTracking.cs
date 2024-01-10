using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class TblClientTracking
    {
        public Guid ClientTrackingId { get; set; }
        public Guid ShipmentId { get; set; }
        public Guid? UserId { get; set; }
        public DateTimeOffset? TrackingDateTime { get; set; }
        public string? Location { get; set; }

        public virtual TblShipment Shipment { get; set; } = null!;
        public virtual TblUser? User { get; set; }
    }
}

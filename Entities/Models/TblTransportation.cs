using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class TblTransportation
    {
        public Guid TransportationId { get; set; }
        public Guid? PackageId { get; set; }
        public Guid? ShipmentId { get; set; }
        public Guid? TransporterUserId { get; set; }
        public DateTimeOffset? DepartureDateTime { get; set; }
        public DateTimeOffset? ArrivalDateTime { get; set; }
        public DateTimeOffset? EstimatedArrivaleDateTime { get; set; }

        public virtual TblPackage? Package { get; set; }
        public virtual TblShipment? Shipment { get; set; }
        public virtual TblUser? TransporterUser { get; set; }
    }
}

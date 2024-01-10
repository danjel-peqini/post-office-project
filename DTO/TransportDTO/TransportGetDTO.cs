using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.TransportDTO
{
    public class TransportGetDTO
    {
        public Guid TransportationId { get; set; }
        public Guid? PackageId { get; set; }
        public Guid? ShipmentId { get; set; }
        public Guid? TransporterUserId { get; set; }
        public DateTimeOffset? DepartureDateTime { get; set; }
        public DateTimeOffset? ArrivalDateTime { get; set; }
        public DateTimeOffset? EstimatedArrivaleDateTime { get; set; }
    }

    public class TransportPostDTO
    {
        public Guid? PackageId { get; set; }
        public Guid? ShipmentId { get; set; }
        public DateTimeOffset? EstimatedArrivaleDateTime { get; set; }
    }
}

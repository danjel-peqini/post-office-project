using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ShipmentDTO
{
    public class ShipmentDetailsDTO
    {
        public Guid ShipmentId { get; set; }

    }
    public class ShipmentGetDTO
    {
        public Guid ShipmentId { get; set; }

        public string? Barcode { get; set; }
        public Guid? SendUserId { get; set; }
        public int? Status { get; set; }
        public DateTimeOffset? CreationDate { get; set; }
        public DateTimeOffset? DeliveryDate { get; set; }
        public double? Price { get; set; }
        public ReceiverDetails ReceiverUserDetails { get; set; }
        public Guid? CreatedBy { get; set; }


    }
    public class ShipmentPostDTO
    {
        public ReceiverDetails ReceiverUserDetails { get; set; }
        public DateTimeOffset? DeliveryDate { get; set; }
        public double? Price { get; set; }
        public Guid? SendUserId { get; set; }
        public string? DeliverPlace { get; set; }

    }
    public class ReceiverDetails
    {
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}

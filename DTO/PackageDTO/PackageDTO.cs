using DTO.ShipmentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.PackageDTO
{
    public class PackageDetailsDTO
    {
        public Guid PackageId { get; set; }
        public int? Status { get; set; }
        public Guid? PostOfficeId { get; set; }
        public List<ShipmentDetailsDTO> TblShipments { get; set; }
    }
    public class PackageDTO
    {
        public Guid PackageId { get; set; }
        public int? Status { get; set; }
        public string? Barcode { get; set; }
        public Guid? UserCreatedId { get; set; }
        public Guid? PostOfficeId { get; set; }
    }
    public class PackagePostDTO
    {
        public Guid DestinationPostOfficeID { get; set; }
        public Guid SourcePostOfficeID { get; set; }

    }
}

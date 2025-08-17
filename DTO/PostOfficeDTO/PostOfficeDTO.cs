using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.PostOfficeDTO
{
    public class PostOfficeDTO
    {
        public Guid PostOfficeId { get; set; }
        public string? Name { get; set; }
        public string? address { get; set; }
        public string? postalCode { get; set; }
        public bool? IsActive { get; set; }
    }
    public class PostOfficePostDTO
    {
        public string? Name { get; set; }
        public string? address { get; set; }
        public string? postalCode { get; set; }

    }
}

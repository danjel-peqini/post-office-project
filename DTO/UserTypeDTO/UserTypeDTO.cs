using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserTypeDTO
{
    public class UserTypeDTO
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string? Name { get; set; }
    }
    public class UserTypeGetDTO
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public string? Name { get; set; }
    }

    public class UserTypePostDto
    {
        public string? Name { get; set; }

    }
}

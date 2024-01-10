using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDTO
{
    public class UserGetDTO
    {
        public Guid UserId { get; set; }
        public string? Username { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsActive { get; set; }
        public Guid UserTypeId { get; set; }
    }
    public class UserPostDTO
    {
        public string? Username { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Guid UserTypeId { get; set; }

    }
    public class UserPutDTO
    {
        public Guid UserTypeId { get; set; }
        public bool IsActive { get; set; }

    }
}

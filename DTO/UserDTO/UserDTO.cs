using DTO.UserTypeDTO;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDTO
{
    public class UserGetDTO
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTimeOffset? Birthday { get; set; }
        public string? Address { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public EntityStatus Status { get; set; }
        public Guid UserTypeId { get; set; }
        public UserTypeGetDTO UserType { get; set; } = null!;
    }
    public class UserPostDTO
    {
        public string? Username { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTimeOffset? Birthday { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; } = null!;
        public Guid UserTypeId { get; set; }

    }
    public class UserPutDTO
    {
        public Guid UserTypeId { get; set; }
        public EntityStatus Status { get; set; }

    }

    public class UserPatchDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTimeOffset? Birthday { get; set; }
        public string? Address { get; set; }
        public Guid? UserTypeId { get; set; }
    }


}

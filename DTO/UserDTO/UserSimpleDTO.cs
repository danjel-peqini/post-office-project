using Helpers;
using System;

namespace DTO.UserDTO
{
    public class UserSimpleDTO
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
    }
}

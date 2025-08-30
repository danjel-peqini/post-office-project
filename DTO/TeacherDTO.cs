using System;
using DTO.UserDTO;
using Helpers;

namespace DTO
{
    public class TeacherDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserSimpleDTO User { get; set; } = null!;
        public EntityStatus Status { get; set; }
    }

    public class TeacherPostDTO
    {
        public Guid? UserId { get; set; }
        public EntityStatus? Status { get; set; }
    }
}

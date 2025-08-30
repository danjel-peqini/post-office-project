using System;
using DTO.UserDTO;

namespace DTO
{
    public class TeacherDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserSimpleDTO User { get; set; } = null!;
    }

    public class TeacherPostDTO
    {
        public Guid? UserId { get; set; }
    }
}

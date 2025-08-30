using System;
using System.Collections.Generic;

namespace DTO
{
    public class GroupStudentPostDTO
    {
        public List<Guid> StudentIds { get; set; } = new();
    }
}

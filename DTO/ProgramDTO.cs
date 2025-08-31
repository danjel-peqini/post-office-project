using System;
using Helpers;

namespace DTO
{
    public class ProgramDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DepartmantDTO Departmant { get; set; }
        public EntityStatus Status { get; set; }
    }

    public class ProgramPostDTO
    {
        public string? Name { get; set; }
        public Guid? DepartmantId { get; set; }
        public EntityStatus? Status { get; set; }
    }
}

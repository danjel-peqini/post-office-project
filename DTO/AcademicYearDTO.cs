using System;
using Helpers;

namespace DTO
{
    public class AcademicYearDTO
    {
        public Guid Id { get; set; }
        public string Year { get; set; } = null!;
        public int StartingYear { get; set; }
        public EntityStatus Status { get; set; }
    }

    public class AcademicYearPostDTO
    {
        public int StartingYear { get; set; }
        public string? Year { get; set; }
        public EntityStatus? Status { get; set; }
    }
}

using System;

namespace DTO
{
    public class AcademicYearDTO
    {
        public Guid Id { get; set; }
        public string Year { get; set; } = null!;
        public bool? IsActive { get; set; }
    }

    public class AcademicYearPostDTO
    {
        public string? Year { get; set; }
        public bool? IsActive { get; set; }
    }
}

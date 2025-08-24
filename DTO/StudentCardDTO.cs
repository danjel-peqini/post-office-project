using System;

namespace DTO
{
    public class StudentCardDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid AcademicYearId { get; set; }
        public string StudentCardCode { get; set; } = null!;
    }

    public class StudentCardPostDTO
    {
        public Guid? UserId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? AcademicYearId { get; set; }
        public string? StudentCardCode { get; set; }
    }
}

using System;
using Helpers;

namespace DTO
{
    public class StudentCardDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid AcademicYearId { get; set; }
        public string StudentCardCode { get; set; } = null!;
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? DisabledDate { get; set; }
        public EntityStatus Status { get; set; }
    }

    public class StudentCardPostDTO
    {
        public Guid? UserId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? AcademicYearId { get; set; }
    }

    public class StudentCardStatusUpdateDTO
    {
        public EntityStatus Status { get; set; }
    }
}

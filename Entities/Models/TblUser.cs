using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class TblUser
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public Guid UserTypeId { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public string Username { get; set; } = null!;
        public string? LastName { get; set; }
        public DateTimeOffset? Birthday { get; set; }
        public string? Address { get; set; }

        public virtual TblUserType UserType { get; set; } = null!;
        public virtual TblStudentCard TblStudentCard { get; set; } = null!;
        public virtual TblTeacher TblTeacher { get; set; } = null!;
    }
}

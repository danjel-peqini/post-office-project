using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

namespace DTO
{
    public class CourseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ProgramDTO Program { get; set; }
        public EntityStatus Status { get; set; }
        public int Credits { get; set; }
        public int? TotalHours { get; set; }
    }
    public class CoursePostDTO
    {
        public string? Name { get; set; } = null!;
        public Guid? ProgramId{ get; set; }
        public EntityStatus? Status { get; set; }
        public int? Credits { get; set; }
        public int? TotalHours { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CourseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DepartmantDTO Departmant { get; set; }
        public bool IsActive { get; set; }
        public int Credits { get; set; }
        public int? TotalHours { get; set; }
    }
    public class CoursePostDTO
    {
        public string? Name { get; set; } = null!;
        public Guid? DepartmantId{ get; set; }
        public bool? IsActive { get; set; }
        public int? Credits { get; set; }
        public int? TotalHours { get; set; }
    }
}

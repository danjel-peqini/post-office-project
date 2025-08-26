using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DepartmantDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string? Description { get; set; }
        public bool isActive { get; set; }
    }
    public class DepartmantPostDTO
    {
        public string? Name { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string? Abbreviation { get; set; }
        public string? Description { get; set; }
    }
}

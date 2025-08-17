using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DepartmantDTO
    {
        private Guid Id { get; set; }
        private string Name { get; set; }
        private bool isActive { get; set; }
    }
    public class DepartmantPostDTO
    {
        private string Name { get; set; }
    }
}

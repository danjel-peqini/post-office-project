using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class TblUserType
    {
        public TblUserType()
        {
            TblUsers = new HashSet<TblUser>();
        }

        public Guid UserTypeId { get; set; }
        public string? Name { get; set; }
        public string? IsActive { get; set; }

        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}

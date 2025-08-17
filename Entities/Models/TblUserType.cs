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

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}

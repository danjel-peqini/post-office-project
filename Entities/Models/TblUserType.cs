using System;
using System.Collections.Generic;
using Helpers;

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
        public EntityStatus Status { get; set; }

        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}

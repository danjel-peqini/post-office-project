using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class TblPostOffice
    {
        public TblPostOffice()
        {
            TblPackageDestinationPostOffices = new HashSet<TblPackage>();
            TblPackageSourcePostOffices = new HashSet<TblPackage>();
        }

        public Guid PostOfficeId { get; set; }
        public string? Name { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<TblPackage> TblPackageDestinationPostOffices { get; set; }
        public virtual ICollection<TblPackage> TblPackageSourcePostOffices { get; set; }
    }
}

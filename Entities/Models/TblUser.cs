using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblPackages = new HashSet<TblPackage>();
            TblShipmentCreatedByNavigations = new HashSet<TblShipment>();
            TblShipmentSendUsers = new HashSet<TblShipment>();
            TblTransportations = new HashSet<TblTransportation>();
        }

        public Guid UserId { get; set; }
        public string? Username { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsActive { get; set; }
        public Guid UserTypeId { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? LastDateModified { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public virtual TblUserType UserType { get; set; } = null!;
        public virtual ICollection<TblPackage> TblPackages { get; set; }
        public virtual ICollection<TblShipment> TblShipmentCreatedByNavigations { get; set; }
        public virtual ICollection<TblShipment> TblShipmentSendUsers { get; set; }
        public virtual ICollection<TblTransportation> TblTransportations { get; set; }
    }
}

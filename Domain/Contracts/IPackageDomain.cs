using DTO.PackageDTO;
using DTO.PostOfficeDTO;
using Helpers.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IPackageDomain
    {
        List<PackageDTO> GetAll();
        void Add(PackagePostDTO packagePostDTO);
        PackageDetailsDTO GetById(Guid packageId);
        void UpdateStatusShipment(Guid packageId, int status);
    }
}

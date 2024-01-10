using DTO.ShipmentDTO;
using Helpers.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IShipmentDomain
    {
        void AddNewShipment(ShipmentPostDTO shipmentPostDTO);
        void UpdateShipment(List<Guid> shipmentIds, Guid packageId);
        ShipmentGetDTO GetById(Guid shipmentId);
        Pagination<ShipmentGetDTO> GetAll(QueryParameters queryParameters, string? searchValue);
        void CancelShipment(Guid shipmentId, int status);
    }
}

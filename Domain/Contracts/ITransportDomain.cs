using DTO.TransportDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ITransportDomain
    {
        List<TransportGetDTO> GetByUserId();
        void AddNewTransport(TransportPostDTO transportPostDTO);
    }
}

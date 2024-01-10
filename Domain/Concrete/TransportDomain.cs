using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.TransportDTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    internal class TransportDomain : DomainBase, ITransportDomain
    {
        private ITransportRepository TransportRepository => _unitOfWork.GetRepository<ITransportRepository>();

        public TransportDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        public List<TransportGetDTO> GetByUserId()
        {
            return _mapper.Map<List<TransportGetDTO>>(TransportRepository.GetById(GetUserId()));
        }

        public void AddNewTransport(TransportPostDTO transportPostDTO)
        {
            var mapper = _mapper.Map<TblTransportation>(transportPostDTO);
            mapper.TransportationId = Guid.NewGuid();
            mapper.DepartureDateTime = DateTime.Now;
            //mapper.TransporterUserId = GetUserId();
            TransportRepository.Add(mapper);
            _unitOfWork.Save();
        }
    }
}

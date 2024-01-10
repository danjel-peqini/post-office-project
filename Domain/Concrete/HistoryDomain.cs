using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.HistoryDTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    internal class HistoryDomain : DomainBase, IHistoryDomain
    {
        private IHistoryRepository HistoryRepository => _unitOfWork.GetRepository<IHistoryRepository>();

        public HistoryDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        public void AddLogs(Dictionary<string, string> keyValuePairs, int actionType)
        {
            keyValuePairs.TryGetValue("Description", out string description);
            keyValuePairs.TryGetValue("CreatedBy", out string createdBy);

            TblHistory tblHistory = new TblHistory()
            {
                HistoryId = Guid.NewGuid(),
                Description = description,
                CreatedBy = Guid.Parse(createdBy),    
                ActionId = actionType,
            };
            HistoryRepository.Add(tblHistory);
            _unitOfWork.Save();

        }
    }
}

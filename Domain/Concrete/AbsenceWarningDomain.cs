using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Domain.Concrete
{
    internal class AbsenceWarningDomain : DomainBase, IAbsenceWarningDomain
    {
        public AbsenceWarningDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private IAbsenceWarningRepository AbsenceWarningRepository => _unitOfWork.GetRepository<IAbsenceWarningRepository>();

        public IEnumerable<AbsenceWarningDTO> GetAll()
        {
            var items = AbsenceWarningRepository.GetAll();
            return _mapper.Map<IEnumerable<AbsenceWarningDTO>>(items);
        }

        public IEnumerable<AbsenceWarningDTO> GetByStudentId(Guid studentId)
        {
            var items = AbsenceWarningRepository.Find(a => a.StudentId == studentId);
            return _mapper.Map<IEnumerable<AbsenceWarningDTO>>(items);
        }
    }
}

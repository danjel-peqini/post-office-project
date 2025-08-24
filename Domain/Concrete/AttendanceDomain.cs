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
    internal class AttendanceDomain : DomainBase, IAttendanceDomain
    {
        public AttendanceDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private IAttendanceRepository AttendanceRepository => _unitOfWork.GetRepository<IAttendanceRepository>();

        public AttendanceDTO CheckIn(AttendanceCheckInDTO dto)
        {
            var entity = AttendanceRepository.CheckIn(dto.StudentCardCode, dto.SessionId);
            return _mapper.Map<AttendanceDTO>(entity);
        }

        public IEnumerable<AttendanceDTO> GetByStudentCardId(Guid studentCardId)
        {
            var items = AttendanceRepository.GetByStudent(studentCardId);
            return _mapper.Map<IEnumerable<AttendanceDTO>>(items);
        }
    }
}

using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO;
using Microsoft.AspNetCore.Http;
using System;

namespace Domain.Concrete
{
    // The domain class is used from the DI registry and needs to be
    // publicly visible so the container can resolve it.  Using the
    // same visibility as the other domain implementations avoids
    // compilation issues when wiring up dependencies.
    public class SessionDomain : DomainBase, ISessionDomain
    {
        public SessionDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private ISessionRepository SessionRepository => _unitOfWork.GetRepository<ISessionRepository>();

        public SessionDTO CreateSession(Guid scheduleId)
        {
            var entity = SessionRepository.CreateSession(scheduleId);
            _unitOfWork.Save();
            return _mapper.Map<SessionDTO>(entity);
        }

        public SessionDTO RegenerateOtp(Guid sessionId)
        {
            var entity = SessionRepository.RegenerateOtp(sessionId);
            _unitOfWork.Save();
            return _mapper.Map<SessionDTO>(entity);
        }

        public SessionDTO CloseSession(Guid sessionId)
        {
            var entity = SessionRepository.CloseSession(sessionId);
            _unitOfWork.Save();
            return _mapper.Map<SessionDTO>(entity);
        }
    }
}

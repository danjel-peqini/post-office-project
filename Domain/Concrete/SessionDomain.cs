using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO;
using Helpers.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Domain.Concrete
{
    internal class SessionDomain : DomainBase, ISessionDomain
    {
        private readonly IConfiguration _configuration;
        public SessionDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(unitOfWork, mapper, httpContextAccessor)
        {
            _configuration = configuration;
        }

        private ISessionRepository SessionRepository => _unitOfWork.GetRepository<ISessionRepository>();

        public SessionDTO CreateSession(Guid scheduleId)
        {
            var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(ipAddress))
                throw new Exception("Unable to determine IP address");
            var allowedIps = _configuration.GetSection("AllowedIPs").Get<List<string>>() ?? new List<string>();
            if (!allowedIps.Contains(ipAddress))
                throw new Exception("IP address not allowed");
            var entity = SessionRepository.CreateSession(scheduleId, ipAddress);
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

        public Pagination<SessionDTO> GetAllSessions(QueryParameters queryParameters, Guid? teacherId)
        {
            queryParameters ??= new QueryParameters();
            var sessions = SessionRepository.GetSessions(queryParameters, teacherId);
            var paginatedData = Pagination<SessionDTO>.ToPagedList(
                sessions,
                src => _mapper.Map<List<SessionDTO>>(src) ?? new List<SessionDTO>());

            return paginatedData;
        }

        public SessionDTO GetSessionById(Guid sessionId)
        {
            var session = SessionRepository.GetById(sessionId);
            if (session == null)
            {
                throw new Exception("Session not found");
            }
            return _mapper.Map<SessionDTO>(session);
        }
    }
}

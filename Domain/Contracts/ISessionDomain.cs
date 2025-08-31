using System;
using DTO;
using Helpers.Pagination;

namespace Domain.Contracts
{
    public interface ISessionDomain
    {
        SessionDTO CreateSession(Guid scheduleId);
        SessionDTO RegenerateOtp(Guid sessionId);
        SessionDTO CloseSession(Guid sessionId);
        Pagination<SessionDTO> GetAllSessions(QueryParameters queryParameters, Guid? teacherId);
        SessionDTO GetSessionById(Guid sessionId);
    }
}

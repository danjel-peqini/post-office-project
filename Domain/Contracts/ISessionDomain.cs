using System;
using DTO;

namespace Domain.Contracts
{
    public interface ISessionDomain
    {
        SessionDTO CreateSession(Guid scheduleId);
        SessionDTO RegenerateOtp(Guid sessionId);
        SessionDTO CloseSession(Guid sessionId);
    }
}

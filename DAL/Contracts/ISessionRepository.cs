using Entities.Models;
using Helpers.Pagination;
using System;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Contracts
{
    public interface ISessionRepository : IRepository<TblSession, Guid>
    {
        TblSession CreateSession(Guid scheduleId);
        TblSession RegenerateOtp(Guid sessionId);
        TblSession CloseSession(Guid sessionId);
        PagedList<TblSession> GetSessions(QueryParameters queryParameters, Guid? teacherId = null);
    }
}

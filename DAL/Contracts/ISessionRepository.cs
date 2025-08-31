using Entities.Models;
using Helpers.Pagination;
using System;
using System.Collections.Generic;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Contracts
{
    public interface ISessionRepository : IRepository<TblSession, Guid>
    {
        TblSession CreateSession(Guid scheduleId);
        TblSession RegenerateOtp(Guid sessionId);
        TblSession CloseSession(Guid sessionId);
        PagedList<TblSession> GetSessions(QueryParameters queryParameters, Guid? teacherId = null);
        IEnumerable<Guid> GetSessionIdsByCourseAndGroup(Guid courseId, Guid groupId, Guid academicYearId);
    }
}

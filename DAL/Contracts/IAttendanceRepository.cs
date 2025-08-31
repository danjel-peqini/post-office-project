using Entities.Models;
using System;
using System.Collections.Generic;

namespace DAL.Contracts
{
    public interface IAttendanceRepository : IRepository<TblAttendance, Guid>
    {
        TblAttendance CheckIn(string studentCardCode, Guid sessionId, string requestIp);
        IEnumerable<TblAttendance> GetByStudent(Guid studentCardId);
        bool HasAttendance(Guid sessionId, Guid studentId);
        int CountAttendances(Guid studentId, IEnumerable<Guid> sessionIds);
    }
}

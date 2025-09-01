using Entities.Models;
using System;
using System.Collections.Generic;

namespace DAL.Contracts
{
    public interface IAttendanceRepository : IRepository<TblAttendance, Guid>
    {
        TblAttendance CheckIn(Guid studentId, string sessionOtp, string requestIp);
        TblAttendance Scan(string studentCardCode, Guid sessionId);
        IEnumerable<TblAttendance> GetByStudent(Guid studentCardId);
        IEnumerable<TblAttendance> GetBySession(Guid sessionId);
        TblAttendance AddAttendance(Guid sessionId, Guid studentId, Guid teacherId);
        void RemoveAttendance(Guid attendanceId);
        bool HasAttendance(Guid sessionId, Guid studentId);
        int CountAttendances(Guid studentId, IEnumerable<Guid> sessionIds);
        IEnumerable<CourseAttendanceSummary> GetCourseAttendanceByStudent(Guid studentId);
        IEnumerable<GroupCourseAttendanceSummary> GetGroupCourseAttendance(Guid groupId, Guid courseId);
    }
}

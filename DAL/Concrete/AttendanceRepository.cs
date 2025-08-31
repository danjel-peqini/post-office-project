using DAL.Contracts;
using Entities.Models;
using Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Concrete
{
    internal class AttendanceRepository : BaseRepository<TblAttendance, Guid>, IAttendanceRepository
    {
        private readonly SchoolAdministrationContext _dbContext;

        public AttendanceRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public TblAttendance CheckIn(string studentCardCode, Guid sessionId, string requestIp)
        {
            var student = _dbContext.TblStudentCards
                .Include(s => s.TblGroupStudents)
                .FirstOrDefault(x => x.StudentCardCode == studentCardCode);
            if (student == null) throw new Exception("Student card not found");

            var session = _dbContext.TblSessions
                .Include(s => s.Schedule)
                .ThenInclude(sc => sc.Group)
                .FirstOrDefault(s => s.Id == sessionId && s.Status != EntityStatus.Deleted);
            if (session == null) throw new Exception("Session not found");

            if (string.IsNullOrEmpty(session.IpAddress))
                throw new Exception("Session network not configured");
            if (session.IpAddress != requestIp)
                throw new Exception("Invalid network");

            var belongs = _dbContext.TblGroupStudents.Any(gs => gs.GroupId == session.Schedule.GroupId && gs.StudentId == student.Id);
            if (!belongs) throw new Exception("Student not in this session");

            var already = _dbContext.TblAttendances.Any(a => a.SessionId == sessionId && a.StudentId == student.Id && a.Status != EntityStatus.Deleted);
            if (already) throw new Exception("Student already checked in");

            var attendance = new TblAttendance
            {
                Id = Guid.NewGuid(),
                SessionId = sessionId,
                StudentId = student.Id,
                CheckInTime = DateTimeOffset.UtcNow,
                CheckedInBy = "student",
                Status = EntityStatus.Active
            };
            Add(attendance);
            _dbContext.SaveChanges();
            return attendance;
        }

        public IEnumerable<TblAttendance> GetByStudent(Guid studentCardId)
        {
            return _dbContext.TblAttendances.Where(a => a.StudentId == studentCardId && a.Status != EntityStatus.Deleted).AsNoTracking().ToList();
        }

        public IEnumerable<TblAttendance> GetBySession(Guid sessionId)
        {
            return _dbContext.TblAttendances
                .Include(a => a.Student).ThenInclude(b => b.User)
                .Where(a => a.SessionId == sessionId && a.Status != EntityStatus.Deleted).AsNoTracking().ToList();
        }

        public TblAttendance AddAttendance(Guid sessionId, Guid studentId, Guid teacherId)
        {
            var session = _dbContext.TblSessions
                .Include(s => s.Schedule)
                .FirstOrDefault(s => s.Id == sessionId && s.Status != EntityStatus.Deleted);
            if (session == null) throw new Exception("Session not found");

            var belongs = _dbContext.TblGroupStudents.Any(gs => gs.GroupId == session.Schedule.GroupId && gs.StudentId == studentId);
            if (!belongs) throw new Exception("Student not in this session");

            var already = _dbContext.TblAttendances.Any(a => a.SessionId == sessionId && a.StudentId == studentId && a.Status != EntityStatus.Deleted);
            if (already) throw new Exception("Student already checked in");

            var attendance = new TblAttendance
            {
                Id = Guid.NewGuid(),
                SessionId = sessionId,
                StudentId = studentId,
                CheckInTime = DateTimeOffset.UtcNow,
                CheckedInBy = $"teacher:{teacherId}",
                Status = EntityStatus.Active
            };
            Add(attendance);
            _dbContext.SaveChanges();
            return attendance;
        }

        public void RemoveAttendance(Guid attendanceId)
        {
            var entity = _dbContext.TblAttendances.FirstOrDefault(a => a.Id == attendanceId);
            _dbContext.TblAttendances.Remove(entity);
            _dbContext.SaveChanges();
        }

        public bool HasAttendance(Guid sessionId, Guid studentId)
        {
            return context.Any(a => a.SessionId == sessionId && a.StudentId == studentId && a.Status != EntityStatus.Deleted);
        }

        public int CountAttendances(Guid studentId, IEnumerable<Guid> sessionIds)
        {
            return context.Count(a => a.StudentId == studentId && sessionIds.Contains(a.SessionId) && a.Status != EntityStatus.Deleted);
        }
    }
}

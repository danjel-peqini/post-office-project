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
            return _dbContext.TblAttendances.Where(a => a.SessionId == sessionId && a.Status != EntityStatus.Deleted).AsNoTracking().ToList();
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
            var entity = _dbContext.TblAttendances.FirstOrDefault(a => a.Id == attendanceId && a.Status != EntityStatus.Deleted);
            if (entity == null) throw new Exception("Attendance not found");
            Remove(entity);
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

        public IEnumerable<CourseAttendanceSummary> GetCourseAttendanceByStudent(Guid studentId)
        {
            var query = from gs in _dbContext.TblGroupStudents
                        where gs.StudentId == studentId
                        join sc in _dbContext.TblSchedules on gs.GroupId equals sc.GroupId
                        where sc.Status != EntityStatus.Deleted
                        join c in _dbContext.TblCourses on sc.CourseId equals c.Id
                        where c.Status != EntityStatus.Deleted
                        join s in _dbContext.TblSessions on sc.Id equals s.ScheduleId
                        where s.Status != EntityStatus.Deleted
                        join a in _dbContext.TblAttendances.Where(a => a.StudentId == studentId && a.Status != EntityStatus.Deleted)
                            on s.Id equals a.SessionId into att
                        from a in att.DefaultIfEmpty()
                        select new { c.Id, c.Name, Attended = a != null };

            return query
                .AsEnumerable()
                .GroupBy(x => new { x.Id, x.Name })
                .Select(g => new CourseAttendanceSummary
                {
                    CourseId = g.Key.Id,
                    CourseName = g.Key.Name,
                    TotalSessions = g.Count(),
                    AttendedSessions = g.Count(x => x.Attended),
                    MissedSessions = g.Count(x => !x.Attended)
                })
                .ToList();
        }
    }
}

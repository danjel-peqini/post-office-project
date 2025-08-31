using DAL.Contracts;
using Entities.Models;
using Helpers;
using Helpers.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Concrete
{
    internal class SessionRepository : BaseRepository<TblSession, Guid>, ISessionRepository
    {
        private readonly SchoolAdministrationContext _dbContext;

        public SessionRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public TblSession CreateSession(Guid scheduleId)
        {
            var schedule = _dbContext.TblSchedules.FirstOrDefault(s => s.Id == scheduleId && s.Status != EntityStatus.Deleted);
            if (schedule == null) throw new Exception("Schedule not found");

            var now = DateTime.UtcNow;

            // ensure the session is created on the scheduled day
            if (schedule.DayOfWeek != (Helpers.DayOfWeek)now.DayOfWeek)
                throw new Exception("Session can only be created on the scheduled day");

            // ensure the current time is within the scheduled time range
            var start = schedule.StartTime.TimeOfDay;
            var end = schedule.EndTime.TimeOfDay;
            if (now.TimeOfDay < start || now.TimeOfDay > end)
                throw new Exception("Session can only be created within the scheduled time range");

            var today = now.Date;
            var sessionDate = today.Add(schedule.StartTime.TimeOfDay);

            var session = new TblSession
            {
                Id = Guid.NewGuid(),
                ScheduleId = scheduleId,
                Date = sessionDate,
                IsOpen = true,
                Otp = GenerateOtp(),
                OtpcreatedAt = DateTime.UtcNow,
                Status = EntityStatus.Active
            };

            return Add(session);
        }

        public TblSession RegenerateOtp(Guid sessionId)
        {
            var session = context.FirstOrDefault(s => s.Id == sessionId && s.Status != EntityStatus.Deleted);
            if (session == null) throw new Exception("Session not found");

            session.Otp = GenerateOtp();
            session.OtpcreatedAt = DateTime.UtcNow;
            Update(session);
            return session;
        }

        public TblSession CloseSession(Guid sessionId)
        {
            var session = context.FirstOrDefault(s => s.Id == sessionId && s.Status != EntityStatus.Deleted);
            if (session == null) throw new Exception("Session not found");

            session.IsOpen = false;
            Update(session);
            return session;
        }

        public PagedList<TblSession> GetSessions(QueryParameters queryParameters, Guid? teacherId = null)
        {
            var data = context
                .Include(s => s.Schedule).ThenInclude(sc => sc.Course).ThenInclude(c => c.Program)
                .Include(s => s.Schedule).ThenInclude(sc => sc.Group)
                .Include(s => s.Schedule).ThenInclude(sc => sc.Teacher).ThenInclude(t => t.User)
                .Include(s => s.Schedule).ThenInclude(sc => sc.Room)
                .Include(s => s.Schedule).ThenInclude(sc => sc.AcademicYear)
                .Where(s => s.Status != EntityStatus.Deleted);

            if (teacherId.HasValue)
            {
                data = data.Where(s => s.Schedule.TeacherId == teacherId.Value);
            }

            var filterData = PaginationConfiguration(
                data,
                queryParameters?.SortField,
                queryParameters?.SortOrder,
                queryParameters?.SearchValue);

            return PagedList<TblSession>.ToPagedList(
                filterData,
                queryParameters?.CurrentPage ?? 1,
                queryParameters?.PageSize ?? 10);
        }

        public override TblSession GetById(Guid id)
        {
            return context
                .Include(s => s.Schedule).ThenInclude(sc => sc.Course).ThenInclude(c => c.Program)
                .Include(s => s.Schedule).ThenInclude(sc => sc.Group)
                .Include(s => s.Schedule).ThenInclude(sc => sc.Teacher).ThenInclude(t => t.User)
                .Include(s => s.Schedule).ThenInclude(sc => sc.Room)
                .Include(s => s.Schedule).ThenInclude(sc => sc.AcademicYear)
                .FirstOrDefault(s => s.Id == id && s.Status != EntityStatus.Deleted);
        }

        public IEnumerable<Guid> GetSessionIdsByCourseAndGroup(Guid courseId, Guid groupId, Guid academicYearId)
        {
            return context
                .Include(s => s.Schedule)
                .Where(s => s.Schedule.CourseId == courseId
                         && s.Schedule.GroupId == groupId
                         && s.Schedule.AcademicYearId == academicYearId
                         && s.Status != EntityStatus.Deleted)
                .Select(s => s.Id)
                .ToList();
        }

        private string GenerateOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}

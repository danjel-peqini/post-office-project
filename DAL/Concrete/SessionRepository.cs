using DAL.Contracts;
using Entities.Models;
using Helpers;
using System;
using System.Linq;

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

            var today = DateTime.UtcNow.Date;
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

        private string GenerateOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}

using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO;
using Helpers.Email;
using Helpers.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    internal class SessionDomain : DomainBase, ISessionDomain
    {
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        public SessionDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, EmailService emailService, IConfiguration configuration) : base(unitOfWork, mapper, httpContextAccessor)
        {
            _emailService = emailService;
            _configuration = configuration;
        }

        private ISessionRepository SessionRepository => _unitOfWork.GetRepository<ISessionRepository>();
        private IGroupStudentRepository GroupStudentRepository => _unitOfWork.GetRepository<IGroupStudentRepository>();
        private IAttendanceRepository AttendanceRepository => _unitOfWork.GetRepository<IAttendanceRepository>();
        private IStudentCardRepository StudentCardRepository => _unitOfWork.GetRepository<IStudentCardRepository>();
        private IAbsenceWarningRepository AbsenceWarningRepository => _unitOfWork.GetRepository<IAbsenceWarningRepository>();

        public SessionDTO CreateSession(Guid scheduleId)
        {
            var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(ipAddress))
                throw new Exception("Unable to determine IP address");
            var allowedIps = _configuration.GetSection("AllowedIPs").Get<List<string>>() ?? new List<string>();
            if (!allowedIps.Contains(ipAddress))
                throw new Exception("IP address not allowed");
            var entity = SessionRepository.CreateSession(scheduleId, ipAddress);
            _unitOfWork.Save();
            return _mapper.Map<SessionDTO>(entity);
        }

        public SessionDTO RegenerateOtp(Guid sessionId)
        {
            var entity = SessionRepository.RegenerateOtp(sessionId);
            _unitOfWork.Save();
            return _mapper.Map<SessionDTO>(entity);
        }

        public async Task<SessionDTO> CloseSession(Guid sessionId)
        {
            var entity = SessionRepository.CloseSession(sessionId);
            _unitOfWork.Save();

            var session = SessionRepository.GetById(sessionId);
            if (session == null)
            {
                throw new Exception("Session not found");
            }

            var groupId = session.Schedule.GroupId;
            var courseId = session.Schedule.CourseId;
            var academicYearId = session.Schedule.AcademicYearId;

            var studentIds = GroupStudentRepository.GetStudentIdsByGroupId(groupId);
            var sessionIds = SessionRepository.GetSessionIdsByCourseAndGroup(courseId, groupId, academicYearId).ToList();

            var courseTotalHours = session.Schedule.Course.TotalHours ?? 0;
            var sessionDuration = (session.Schedule.EndTime - session.Schedule.StartTime).TotalHours;
            var totalSessions = sessionDuration == 0 ? 0 : (int)Math.Ceiling(courseTotalHours / sessionDuration);

            foreach (var studentId in studentIds)
            {
                if (AttendanceRepository.HasAttendance(sessionId, studentId))
                {
                    continue;
                }

                var attendedCount = AttendanceRepository.CountAttendances(studentId, sessionIds);
                var missedCount = Math.Max(0, totalSessions - attendedCount);
                double attendancePercentage = totalSessions == 0 ? 0 : (double)attendedCount / totalSessions * 100;
                double absencePercentage = totalSessions == 0 ? 0 : (double)missedCount / totalSessions * 100;

                if (attendancePercentage < 80)
                {
                    AbsenceWarningRepository.AddOrUpdate(studentId, courseId, absencePercentage);

                    var student = StudentCardRepository.GetById(studentId);
                    if (student?.User?.Email != null)
                    {
                        await _emailService.SendEmail(
                            student.User.Email,
                            "Attendance Warning",
                            $"Hello {student.User.FirstName} {student.User.LastName},<br/>You have attended {attendedCount} out of {totalSessions} sessions for {session.Schedule.Course.Name} and missed {missedCount}. Your attendance is {attendancePercentage:F2}%. Please attend classes to avoid further actions.");
                    }
                }
            }

            _unitOfWork.Save();
            return _mapper.Map<SessionDTO>(session);
        }

        public Pagination<SessionDTO> GetAllSessions(QueryParameters queryParameters, Guid? teacherId)
        {
            queryParameters ??= new QueryParameters();
            var sessions = SessionRepository.GetSessions(queryParameters, teacherId);
            var paginatedData = Pagination<SessionDTO>.ToPagedList(
                sessions,
                src => _mapper.Map<List<SessionDTO>>(src) ?? new List<SessionDTO>());

            return paginatedData;
        }

        public SessionDTO GetSessionById(Guid sessionId)
        {
            var session = SessionRepository.GetById(sessionId);
            if (session == null)
            {
                throw new Exception("Session not found");
            }
            return _mapper.Map<SessionDTO>(session);
        }
    }
}

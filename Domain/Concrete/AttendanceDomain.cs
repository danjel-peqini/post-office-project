using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Domain.Concrete
{
    internal class AttendanceDomain : DomainBase, IAttendanceDomain
    {
        public AttendanceDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private IAttendanceRepository AttendanceRepository => _unitOfWork.GetRepository<IAttendanceRepository>();
        private IGroupStudentRepository GroupStudentRepository => _unitOfWork.GetRepository<IGroupStudentRepository>();
        private IStudentCardRepository StudentCardRepository => _unitOfWork.GetRepository<IStudentCardRepository>();

        public AttendanceDTO CheckIn(AttendanceCheckInDTO dto)
        {
            var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(ipAddress))
                throw new Exception("Unable to determine IP address");
            var entity = AttendanceRepository.CheckIn(dto.StudentId, dto.SessionOtp, ipAddress);
            return _mapper.Map<AttendanceDTO>(entity);
        }

        public AttendanceDTO Scan(AttendanceScanDTO dto)
        {
            var entity = AttendanceRepository.Scan(dto.StudentCardCode, dto.SessionId);
            return _mapper.Map<AttendanceDTO>(entity);
        }

        public IEnumerable<AttendanceDTO> GetByStudentCardId(Guid studentCardId)
        {
            var items = AttendanceRepository.GetByStudent(studentCardId);
            return _mapper.Map<IEnumerable<AttendanceDTO>>(items);
        }

        public IEnumerable<AttendanceDTO> GetBySessionId(Guid sessionId)
        {
            var items = AttendanceRepository.GetBySession(sessionId);
            return _mapper.Map<IEnumerable<AttendanceDTO>>(items);
        }

        public IEnumerable<CourseAttendanceSummaryDTO> GetCourseAttendanceByStudent(Guid studentCardId)
        {
            var items = AttendanceRepository.GetCourseAttendanceByStudent(studentCardId);
            return _mapper.Map<IEnumerable<CourseAttendanceSummaryDTO>>(items);
        }

        public IEnumerable<GroupCourseAttendanceSummaryDTO> GetGroupCourseAttendance(Guid groupId, Guid courseId)
        {
            var items = AttendanceRepository.GetGroupCourseAttendance(groupId, courseId);
            return _mapper.Map<IEnumerable<GroupCourseAttendanceSummaryDTO>>(items);
        }

        public AttendanceDTO AddAttendance(AttendanceAddDTO dto)
        {
            var teacherId = GetUserId();
            var entity = AttendanceRepository.AddAttendance(dto.SessionId, dto.StudentId, teacherId);
            return _mapper.Map<AttendanceDTO>(entity);
        }

        public void RemoveAttendance(Guid attendanceId)
        {
            AttendanceRepository.RemoveAttendance(attendanceId);
        }

        public IEnumerable<GroupSessionAttendanceDTO> GetGroupSessionAttendance(Guid groupId, Guid sessionId)
        {
            var studentIds = GroupStudentRepository.GetStudentIdsByGroupId(groupId);
            var students = StudentCardRepository.GetByIds(studentIds);
            var attendances = AttendanceRepository.GetBySession(sessionId);

            var attendanceDict = new Dictionary<Guid, TblAttendance>();
            foreach (var att in attendances)
            {
                if (!attendanceDict.ContainsKey(att.StudentId))
                    attendanceDict.Add(att.StudentId, att);
            }

            var result = new List<GroupSessionAttendanceDTO>();
            foreach (var student in students)
            {
                var dto = new GroupSessionAttendanceDTO
                {
                    StudentId = student.Id,
                    StudentCardCode = student.StudentCardCode,
                    FirstName = student.User.FirstName,
                    LastName = student.User.LastName,
                    HasAttended = attendanceDict.TryGetValue(student.Id, out var att),
                    CheckInTime = att?.CheckInTime
                };
                result.Add(dto);
            }

            return result;
        }
    }
}

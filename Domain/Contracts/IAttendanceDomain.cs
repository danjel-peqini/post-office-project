using DTO;
using System;
using System.Collections.Generic;

namespace Domain.Contracts
{
    public interface IAttendanceDomain
    {
        AttendanceDTO CheckIn(AttendanceCheckInDTO dto);
        AttendanceDTO Scan(AttendanceScanDTO dto);
        IEnumerable<AttendanceDTO> GetByStudentCardId(Guid studentCardId);
        IEnumerable<AttendanceDTO> GetBySessionId(Guid sessionId);
        AttendanceDTO AddAttendance(AttendanceAddDTO dto);
        void RemoveAttendance(Guid attendanceId);
        IEnumerable<GroupSessionAttendanceDTO> GetGroupSessionAttendance(Guid groupId, Guid sessionId);
        IEnumerable<CourseAttendanceSummaryDTO> GetCourseAttendanceByStudent(Guid studentCardId);
        IEnumerable<GroupCourseAttendanceSummaryDTO> GetGroupCourseAttendance(Guid groupId, Guid courseId);
    }
}

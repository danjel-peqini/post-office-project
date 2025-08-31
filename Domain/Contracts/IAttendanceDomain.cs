using DTO;
using System;
using System.Collections.Generic;

namespace Domain.Contracts
{
    public interface IAttendanceDomain
    {
        AttendanceDTO CheckIn(AttendanceCheckInDTO dto);
        IEnumerable<AttendanceDTO> GetByStudentCardId(Guid studentCardId);
        IEnumerable<AttendanceDTO> GetBySessionId(Guid sessionId);
        AttendanceDTO AddAttendance(AttendanceAddDTO dto);
        void RemoveAttendance(Guid attendanceId);
        IEnumerable<GroupSessionAttendanceDTO> GetGroupSessionAttendance(Guid groupId, Guid sessionId);
    }
}

using DTO;
using System;
using System.Collections.Generic;

namespace Domain.Contracts
{
    public interface IAttendanceDomain
    {
        AttendanceDTO CheckIn(AttendanceCheckInDTO dto);
        IEnumerable<AttendanceDTO> GetByStudentCardId(Guid studentCardId);
    }
}

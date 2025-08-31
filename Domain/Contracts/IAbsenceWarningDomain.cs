using DTO;
using System;
using System.Collections.Generic;

namespace Domain.Contracts
{
    public interface IAbsenceWarningDomain
    {
        IEnumerable<AbsenceWarningDTO> GetAll();
        IEnumerable<AbsenceWarningDTO> GetByStudentId(Guid studentId);
    }
}

using Entities.Models;
using System;

namespace DAL.Contracts
{
    public interface IAbsenceWarningRepository : IRepository<TblAbsenceWarning, Guid>
    {
        TblAbsenceWarning AddOrUpdate(Guid studentId, Guid courseId, double percentage);
    }
}

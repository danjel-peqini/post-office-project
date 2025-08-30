using System;
using System.Collections.Generic;
using Entities.Models;

namespace DAL.Contracts
{
    public interface IGroupStudentRepository : IRepository<TblGroupStudent, Guid>
    {
        IEnumerable<Guid> GetStudentIdsByGroupId(Guid groupId);
    }
}

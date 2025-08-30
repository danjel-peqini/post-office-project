using System;
using Entities.Models;

namespace DAL.Contracts
{
    public interface IGroupStudentRepository : IRepository<TblGroupStudent, Guid>
    {
    }
}

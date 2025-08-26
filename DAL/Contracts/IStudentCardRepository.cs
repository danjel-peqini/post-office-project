using Entities.Models;
using System;

namespace DAL.Contracts
{
    public interface IStudentCardRepository : IRepository<TblStudentCard, Guid>
    {
        TblStudentCard? GetByCode(string cardCode);
    }
}

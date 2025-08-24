using DAL.Contracts;
using Entities.Models;
using System;
using System.Linq;

namespace DAL.Concrete
{
    internal class StudentCardRepository : BaseRepository<TblStudentCard, Guid>, IStudentCardRepository
    {
        public StudentCardRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }

        public TblStudentCard? GetByCode(string cardCode)
        {
            return context.FirstOrDefault(x => x.StudentCardCode == cardCode);
        }
    }
}

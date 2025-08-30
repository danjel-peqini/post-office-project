using System;
using DAL.Contracts;
using Entities.Models;

namespace DAL.Concrete
{
    internal class GroupStudentRepository : BaseRepository<TblGroupStudent, Guid>, IGroupStudentRepository
    {
        public GroupStudentRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }
    }
}

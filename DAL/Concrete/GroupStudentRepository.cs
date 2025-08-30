using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contracts;
using Entities.Models;

namespace DAL.Concrete
{
    internal class GroupStudentRepository : BaseRepository<TblGroupStudent, Guid>, IGroupStudentRepository
    {
        public GroupStudentRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Guid> GetStudentIdsByGroupId(Guid groupId)
        {
            return context
                .Where(x => x.GroupId == groupId)
                .Select(x => x.StudentId)
                .ToList();
        }
    }
}

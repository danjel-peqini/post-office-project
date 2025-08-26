using DAL.Contracts;
using Entities.Models;
using System;

namespace DAL.Concrete
{
    internal class GroupRepository : BaseRepository<TblGroup, Guid>, IGroupRepository
    {
        public GroupRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }
    }
}

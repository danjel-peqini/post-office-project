using DAL.Contracts;
using Entities.Models;
using System;

namespace DAL.Concrete
{
    internal class ScheduleRepository : BaseRepository<TblSchedule, Guid>, IScheduleRepository
    {
        public ScheduleRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }
    }
}

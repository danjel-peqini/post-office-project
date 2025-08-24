using DAL.Concrete;
using Entities.Models;
using System;

namespace DAL.Contracts
{
    public interface IScheduleRepository : IRepository<TblSchedule, Guid>
    {
    }
}

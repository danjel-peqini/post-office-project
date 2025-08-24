using DAL.Concrete;
using Entities.Models;
using System;

namespace DAL.Contracts
{
    public interface IGroupRepository : IRepository<TblGroup, Guid>
    {
    }
}

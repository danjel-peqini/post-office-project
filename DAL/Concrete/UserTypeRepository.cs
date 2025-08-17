using DAL.Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    internal class UserTypeRepository : BaseRepository<TblUserType, Guid>, IUserTypeRepository
    {
        public UserTypeRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }
    }
}

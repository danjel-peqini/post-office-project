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
    internal class TransportRepository : BaseRepository<TblTransportation, Guid>, ITransportRepository
    {
        public TransportRepository(PostOfficeDBContext dbContext) : base(dbContext)
        {
        }

        public List<TblTransportation> GetByUserID(Guid userId)
        {
            return context.Where(x => x.TransporterUserId == userId).ToList();
        }
    }
}

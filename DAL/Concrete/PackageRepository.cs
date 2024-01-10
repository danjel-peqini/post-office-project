using DAL.Contracts;
using Entities.Models;
using Helpers.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Concrete
{
    internal class PackageRepository : BaseRepository<TblPackage, Guid>, IPackageRepository
    {
        public PackageRepository(PostOfficeDBContext dbContext) : base(dbContext)
        {
        }

        public TblPackage GetDetailsById(Guid packageId)
        {
            return context.Include(x => x.TblShipments).FirstOrDefault(x => x.PackageId == packageId);
        }
    }
}

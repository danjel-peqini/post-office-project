using Entities.Models;
using Helpers.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Contracts
{
    public interface IPostOfficeRepository : IRepository<TblPostOffice, Guid>
    {
        TblPostOffice CheckUniquePostOfficeName(string name);
        PagedList<TblPostOffice> GetAll(QueryParameters queryParameters, string? searchValue);
        TblPostOffice GetById(Guid postOfficeId);
    }
}

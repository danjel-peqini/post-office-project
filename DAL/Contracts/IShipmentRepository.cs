using DAL.Concrete;
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
    public interface IShipmentRepository: IRepository<TblShipment, Guid>
    {
        PagedList<TblShipment> GetAll(QueryParameters queryParameters, string? searchValue, Guid userId);
    }
}

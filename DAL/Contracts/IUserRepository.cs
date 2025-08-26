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
    public interface IUserRepository: IRepository<TblUser, Guid>
    {
        PagedList<TblUser> GetAllUsers(QueryParameters queryParameters);
        TblUser CheckIfUsernamExist(string userName);
        List<TblUser> GetUsersByTypeIds(IEnumerable<Guid> userTypeIds);

    }
}

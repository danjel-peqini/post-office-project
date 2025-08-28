using DAL.Contracts;
using Entities.Models;
using Helpers;
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
    internal class UserRepository : BaseRepository<TblUser, Guid>, IUserRepository
    {

        public UserRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }

        public TblUser CheckIfUsernamExist(string userName)
        {
            return context.Include(x=>x.UserType).FirstOrDefault(x => x.Status == EntityStatus.Active && x.Username.ToLower() == userName.ToLower());
        }

        public PagedList<TblUser> GetAllUsers(QueryParameters queryParameters)
        {
            var data = context.Where(x => x.Status == EntityStatus.Active);
            var filterData = PaginationConfiguration(data, queryParameters.SortField, queryParameters.SortOrder, queryParameters.SearchValue);
            return PagedList<TblUser>.ToPagedList(filterData, queryParameters == null ? 1 : queryParameters.CurrentPage, queryParameters == null ? 10 : queryParameters.PageSize);

        }

        public PagedList<TblUser> GetUsersByTypeIds(IEnumerable<Guid> userTypeIds, QueryParameters queryParameters)
        {
            var data = context.Where(x => x.Status == EntityStatus.Active && userTypeIds.Contains(x.UserTypeId));
            var filterData = PaginationConfiguration(data, queryParameters.SortField, queryParameters.SortOrder, queryParameters.SearchValue);
            return PagedList<TblUser>.ToPagedList(filterData, queryParameters.CurrentPage, queryParameters.PageSize);
        }

    }


}

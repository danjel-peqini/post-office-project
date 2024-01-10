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
    internal class UserRepository : BaseRepository<TblUser, Guid>, IUserRepository
    {

        public UserRepository(PostOfficeDBContext dbContext) : base(dbContext)
        {
        }

        public TblUser CheckIfUsernamExist(string userName)
        {
            return context.Include(x=>x.UserType).FirstOrDefault(x => x.IsActive && x.Username.ToLower() == userName.ToLower());
        }

        public PagedList<TblUser> GetAllUsers(QueryParameters queryParameters)
        {
            var data = context.Where(x => x.IsActive);
            var filterData = PaginationConfiguration(data, queryParameters.SortField, queryParameters.SortOrder, queryParameters.SearchValue);
            return PagedList<TblUser>.ToPagedList(filterData, queryParameters == null ? 1 : queryParameters.CurrentPage, queryParameters == null ? 10 : queryParameters.PageSize);

        } 
    }


}

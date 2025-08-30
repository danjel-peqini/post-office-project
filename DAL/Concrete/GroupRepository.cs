using DAL.Contracts;
using Entities.Models;
using Helpers.Pagination;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Concrete
{
    internal class GroupRepository : BaseRepository<TblGroup, Guid>, IGroupRepository
    {
        public GroupRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }

        public PagedList<TblGroup> GetGroups(QueryParameters queryParameters)
        {
            var data = context;
            var filterData = PaginationConfiguration(data, queryParameters.SortField, queryParameters.SortOrder, queryParameters.SearchValue);
            return PagedList<TblGroup>.ToPagedList(filterData, queryParameters == null ? 1 : queryParameters.CurrentPage, queryParameters == null ? 10 : queryParameters.PageSize);
        }
    }
}

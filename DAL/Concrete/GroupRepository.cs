using DAL.Contracts;
using Entities.Models;
using Helpers.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            var data = context
                .Include(g => g.Course)
                .Include(g => g.AcademicYear)
                .Include(g => g.TblGroupStudents);

            var filterData = PaginationConfiguration(data, queryParameters.SortField, queryParameters.SortOrder, queryParameters.SearchValue);
            return PagedList<TblGroup>.ToPagedList(filterData, queryParameters == null ? 1 : queryParameters.CurrentPage, queryParameters == null ? 10 : queryParameters.PageSize);
        }

        public override TblGroup GetById(Guid id)
        {
            return context
                .Include(g => g.Course)
                .Include(g => g.AcademicYear)
                .Include(g => g.TblGroupStudents)
                .FirstOrDefault(g => g.Id == id);
        }
    }
}

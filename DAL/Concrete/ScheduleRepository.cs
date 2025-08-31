using DAL.Contracts;
using Entities.Models;
using Helpers;
using Helpers.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Concrete
{
    internal class ScheduleRepository : BaseRepository<TblSchedule, Guid>, IScheduleRepository
    {
        public ScheduleRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }

        public PagedList<TblSchedule> GetSchedules(QueryParameters queryParameters)
        {
            var data = context
                .Where(s => s.Status != EntityStatus.Deleted)
                .Include(s => s.Course)
                .Include(s => s.Group)
                .Include(s => s.Group).ThenInclude(g => g.TblGroupStudents)
                .Include(s => s.Teacher).ThenInclude(t => t.User)
                .Include(s => s.Room)
                .Include(s => s.AcademicYear);

            // Safely handle null query parameters to avoid null reference exceptions
            var filterData = PaginationConfiguration(
                data,
                queryParameters?.SortField,
                queryParameters?.SortOrder,
                queryParameters?.SearchValue);

            return PagedList<TblSchedule>.ToPagedList(
                filterData,
                queryParameters?.CurrentPage ?? 1,
                queryParameters?.PageSize ?? 10);
        }

        public override TblSchedule GetById(Guid id)
        {
            return context
                .Include(s => s.Course)
                .Include(s => s.Group)
                .Include(s => s.Group).ThenInclude(g => g.TblGroupStudents)
                .Include(s => s.Teacher).ThenInclude(t => t.User)
                .Include(s => s.Room)
                .Include(s => s.AcademicYear)
                .FirstOrDefault(s => s.Id == id && s.Status != EntityStatus.Deleted);
        }
    }
}

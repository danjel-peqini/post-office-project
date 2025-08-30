using System;
using System.Linq;
using DAL.Contracts;
using Entities.Models;
using Helpers;
using Helpers.Pagination;
using Microsoft.EntityFrameworkCore;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Concrete
{
    internal class TeacherRepository : BaseRepository<TblTeacher, Guid>, ITeacherRepository
    {
        public TeacherRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }

        public PagedList<TblTeacher> GetTeachers(QueryParameters queryParameters)
        {
            var data = context.Include(t => t.User).Where(t => t.Status != EntityStatus.Deleted);
            var filterData = PaginationConfiguration(data, queryParameters.SortField, queryParameters.SortOrder, queryParameters.SearchValue);
            return PagedList<TblTeacher>.ToPagedList(filterData, queryParameters == null ? 1 : queryParameters.CurrentPage, queryParameters == null ? 10 : queryParameters.PageSize);
        }

        public override TblTeacher GetById(Guid id)
        {
            return context.Include(t => t.User).FirstOrDefault(t => t.Id == id && t.Status != EntityStatus.Deleted);
        }
    }
}

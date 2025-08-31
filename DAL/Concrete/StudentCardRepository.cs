using DAL.Contracts;
using Entities.Models;
using Helpers;
using Helpers.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static Helpers.Pagination.QueryParameters;

namespace DAL.Concrete
{
    internal class StudentCardRepository : BaseRepository<TblStudentCard, Guid>, IStudentCardRepository
    {
        private readonly SchoolAdministrationContext _dbContext;

        public StudentCardRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void DisableCardsByUser(Guid userId)
        {
            var cards = _dbContext.TblStudentCards.Where(x => x.UserId == userId && x.Status != EntityStatus.Disabled && x.Status != EntityStatus.Deleted).ToList();
            foreach (var card in cards)
            {
                card.Status = EntityStatus.Disabled;
                card.DisabledDate = DateTimeOffset.UtcNow;
            }
        }

        public PagedList<TblStudentCard> GetStudentCards(QueryParameters queryParameters, Guid? userId, Guid? academicYearId, Guid? departmentId)
        {
            var data = context
                .Include(x => x.User)
                .Include(x => x.Department)
                .Include(x => x.AcademicYear)
                .Where(x => x.Status != EntityStatus.Deleted)
                .AsQueryable();
            if (userId.HasValue)
                data = data.Where(x => x.UserId == userId.Value);
            if (academicYearId.HasValue)
                data = data.Where(x => x.AcademicYearId == academicYearId.Value);
            if (departmentId.HasValue)
                data = data.Where(x => x.DepartmentId == departmentId.Value);
            var filterData = PaginationConfiguration(data, queryParameters.SortField, queryParameters.SortOrder, queryParameters.SearchValue);
            return PagedList<TblStudentCard>.ToPagedList(filterData, queryParameters == null ? 1 : queryParameters.CurrentPage, queryParameters == null ? 10 : queryParameters.PageSize);
        }

        public IEnumerable<TblStudentCard> GetByIds(IEnumerable<Guid> ids)
        {
            return context
                .Include(x => x.User)
                .Include(x => x.Department)
                .Include(x => x.AcademicYear)
                .Where(x => ids.Contains(x.Id) && x.Status != EntityStatus.Deleted)
                .ToList();
        }

        public override TblStudentCard GetById(Guid id)
        {
            return context
                .Include(x => x.User)
                .Include(x => x.Department)
                .Include(x => x.AcademicYear)
                .FirstOrDefault(x => x.Id == id && x.Status != EntityStatus.Deleted);
        }
    }
}

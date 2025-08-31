using Entities.Models;
using Helpers.Pagination;
using static Helpers.Pagination.QueryParameters;
using System;
using System.Collections.Generic;

namespace DAL.Contracts
{
    public interface IStudentCardRepository : IRepository<TblStudentCard, Guid>
    {
        PagedList<TblStudentCard> GetStudentCards(QueryParameters queryParameters, Guid? userId, Guid? academicYearId, Guid? departmentId);
        void DisableCardsByUser(Guid userId);
        IEnumerable<TblStudentCard> GetByIds(IEnumerable<Guid> ids);
    }
}

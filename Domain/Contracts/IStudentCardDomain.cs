using DTO;
using Helpers;
using Helpers.Pagination;
using System;

namespace Domain.Contracts
{
    public interface IStudentCardDomain
    {
        Pagination<StudentCardDTO> GetAll(QueryParameters queryParameters, Guid? userId, Guid? academicYearId, Guid? programId);
        StudentCardDTO GetById(Guid id);
        StudentCardDTO GetByUserId(Guid userId);
        void AddNew(StudentCardPostDTO dto);
        StudentCardDTO Update(Guid id, StudentCardPostDTO dto);
        void UpdateStatus(Guid id, EntityStatus status);
        void Delete(Guid id);
    }
}

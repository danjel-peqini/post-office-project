using DTO;
using System;
using System.Collections.Generic;

namespace Domain.Contracts
{
    public interface IStudentCardDomain
    {
        IEnumerable<StudentCardDTO> GetAll();
        StudentCardDTO GetById(Guid id);
        void Add(StudentCardPostDTO dto);
        void Update(Guid id, StudentCardPostDTO dto);
        void Delete(Guid id);
    }
}

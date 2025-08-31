using DTO;
using Helpers.Pagination;

namespace Domain.Contracts
{
    public interface IProgramDomain
    {
        Pagination<ProgramDTO> GetAllPrograms(QueryParameters queryParameters);
        ProgramDTO GetProgramById(Guid id);
        void AddNew(ProgramPostDTO program);
        void Delete(Guid id);
        ProgramDTO Update(Guid id, ProgramPostDTO program);
    }
}

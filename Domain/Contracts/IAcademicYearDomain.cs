using DTO;
using Helpers.Pagination;

namespace Domain.Contracts
{
    public interface IAcademicYearDomain
    {
        Pagination<AcademicYearDTO> GetAllAcademicYears(QueryParameters queryParameters);
        AcademicYearDTO GetAcademicYearById(Guid id);
        void AddNew(AcademicYearPostDTO academicYearPostDTO);
        AcademicYearDTO Update(Guid id, AcademicYearPostDTO academicYearPostDTO);
    }
}

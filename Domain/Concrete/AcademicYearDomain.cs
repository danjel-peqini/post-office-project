using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO;
using Entities.Models;
using Helpers.Pagination;
using Microsoft.AspNetCore.Http;

namespace Domain.Concrete
{
    internal class AcademicYearDomain : DomainBase, IAcademicYearDomain
    {
        public AcademicYearDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private IAcademicYearRepository AcademicYearRepository => _unitOfWork.GetRepository<IAcademicYearRepository>();

        public void AddNew(AcademicYearPostDTO academicYearPostDTO)
        {
            var entity = _mapper.Map<TblAcademicYear>(academicYearPostDTO);
            entity.Id = Guid.NewGuid();
            AcademicYearRepository.Add(entity);
            _unitOfWork.Save();
        }

        public Pagination<AcademicYearDTO> GetAllAcademicYears(QueryParameters queryParameters)
        {
            var years = AcademicYearRepository.GetAcademicYears(queryParameters);
            var paginatedData = Pagination<AcademicYearDTO>.ToPagedList(years, _mapper.Map<List<AcademicYearDTO>>);
            return paginatedData;
        }

        public AcademicYearDTO GetAcademicYearById(Guid id)
        {
            var entity = AcademicYearRepository.GetById(id);
            if (entity == null)
            {
                throw new Exception("Academic year not found");
            }
            return _mapper.Map<AcademicYearDTO>(entity);
        }

        public AcademicYearDTO Update(Guid id, AcademicYearPostDTO academicYearPostDTO)
        {
            var entity = AcademicYearRepository.GetById(id);
            if (entity == null)
            {
                throw new Exception("Academic year not found");
            }
            if (!string.IsNullOrWhiteSpace(academicYearPostDTO.Year))
                entity.Year = academicYearPostDTO.Year;
            if (academicYearPostDTO.IsActive.HasValue)
                entity.IsActive = academicYearPostDTO.IsActive;
            AcademicYearRepository.SetModified(entity);
            _unitOfWork.Save();
            return _mapper.Map<AcademicYearDTO>(entity);
        }

        public void Delete(Guid id)
        {
            var entity = AcademicYearRepository.GetById(id);
            if (entity == null)
            {
                throw new Exception("Academic year not found");
            }
            AcademicYearRepository.Remove(id);
            _unitOfWork.Save();
        }
    }
}

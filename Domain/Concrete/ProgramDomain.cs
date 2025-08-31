using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO;
using Entities.Models;
using Helpers;
using Helpers.Pagination;
using Microsoft.AspNetCore.Http;

namespace Domain.Concrete
{
    internal class ProgramDomain : DomainBase, IProgramDomain
    {
        public ProgramDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private IProgramRepository ProgramRepository => _unitOfWork.GetRepository<IProgramRepository>();

        public void AddNew(ProgramPostDTO program)
        {
            var entity = _mapper.Map<TblProgram>(program);
            entity.Id = Guid.NewGuid();
            entity.Status = program.Status ?? EntityStatus.Active;
            ProgramRepository.Add(entity);
            _unitOfWork.Save();
        }

        public void Delete(Guid id)
        {
            ProgramRepository.Remove(id);
            _unitOfWork.Save();
        }

        public Pagination<ProgramDTO> GetAllPrograms(QueryParameters queryParameters)
        {
            var programs = ProgramRepository.GetPrograms(queryParameters);
            var paginatedData = Pagination<ProgramDTO>.ToPagedList(programs, _mapper.Map<List<ProgramDTO>>);
            return paginatedData;
        }

        public ProgramDTO GetProgramById(Guid id)
        {
            var entity = ProgramRepository.GetById(id);
            if (entity == null)
            {
                throw new Exception("Program not found");
            }
            return _mapper.Map<ProgramDTO>(entity);
        }

        public ProgramDTO Update(Guid id, ProgramPostDTO program)
        {
            var entity = ProgramRepository.GetById(id);
            if (entity == null)
            {
                throw new Exception("Program not found");
            }
            if (!string.IsNullOrWhiteSpace(program.Name))
                entity.Name = program.Name;
            if (program.Status.HasValue)
                entity.Status = program.Status.Value;
            if (program.DepartmantId.HasValue)
                entity.DepartmentId = program.DepartmantId.Value;
            ProgramRepository.SetModified(entity);
            _unitOfWork.Save();
            return _mapper.Map<ProgramDTO>(entity);
        }
    }
}

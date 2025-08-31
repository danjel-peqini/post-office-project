using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO;
using DTO.UserDTO;
using DTO.UserTypeDTO;
using Entities.Models;
using Helpers;
using Helpers.Pagination;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    internal class DepartmantDomain : DomainBase, IDepartmantDomain
    {
        public DepartmantDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }
        private IDepartmantRepository DepartmantRepository => _unitOfWork.GetRepository<IDepartmantRepository>();
        private IProgramRepository ProgramRepository => _unitOfWork.GetRepository<IProgramRepository>();

        public ProgramDTO addProgram(ProgramPostDTO program, Guid departmantId)
        {
            try
            {
                var mapped = _mapper.Map<TblProgram>(program);
                mapped.Status = EntityStatus.Active;
                mapped.DepartmentId = departmantId;
                mapped.Id = Guid.NewGuid();
                ProgramRepository.Add(mapped);
                _unitOfWork.Save();
                return _mapper.Map<ProgramDTO>(mapped);

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNew(DepartmantPostDTO departmantPostDTO)
        {
            try
            {
                var mapper = _mapper.Map<TblDepartment>(departmantPostDTO);
                mapper.CreatedDate = DateTimeOffset.Now;
                mapper.Status = EntityStatus.Active;
                mapper.Id = Guid.NewGuid();
                DepartmantRepository.Add(mapper);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                DepartmantRepository.Remove(id);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Pagination<DepartmantDTO> GetAllDepartmants(QueryParameters queryParameters)
        {
            var users = DepartmantRepository.GetDeparttmants(queryParameters);
            var paginatedData = Pagination<DepartmantDTO>.ToPagedList(users, _mapper.Map<List<DepartmantDTO>>);
            return paginatedData;
        }

        public DepartmantDTO GetDepartmantBtyd(Guid id)
        {
            TblDepartment entity = DepartmantRepository.GetById(id);
            if (entity == null)
            {
                throw new Exception("Departmant not found");
            }
            return _mapper.Map<DepartmantDTO>(entity);
        }

        public DepartmantDTO Update(Guid id, DepartmantPostDTO departmantPostDTO)
        {
            try
            {
                var entity = DepartmantRepository.GetById(id);
                if (entity == null)
                {
                    throw new Exception("Departmant not found");
                }
                if (!string.IsNullOrWhiteSpace(departmantPostDTO.Name))
                    entity.Name = departmantPostDTO.Name;
                if (!string.IsNullOrWhiteSpace(departmantPostDTO.Abbreviation))
                    entity.Abbreviation = departmantPostDTO.Abbreviation;
                if (!string.IsNullOrWhiteSpace(departmantPostDTO.Description))
                    entity.Description = departmantPostDTO.Description;
                if (departmantPostDTO.CreatedDate.HasValue)
                    entity.CreatedDate = departmantPostDTO.CreatedDate.Value;
                DepartmantRepository.SetModified(entity);
                _unitOfWork.Save();
                return _mapper.Map<DepartmantDTO>(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ProgramDTO updateProgram(ProgramPostDTO program, Guid programId)
        {
            try
            {
                TblProgram programEntity = ProgramRepository.GetById(programId);
                if (programEntity == null)
                {
                    throw new Exception("Program not found");
                }
                if (!string.IsNullOrWhiteSpace(program.Name))
                    programEntity.Name = program.Name;
                if (program.Status.HasValue)
                    programEntity.Status = program.Status.Value;
                if (program.DepartmantId.HasValue)
                    programEntity.DepartmentId = program.DepartmantId.Value;
                ProgramRepository.SetModified(programEntity);
                _unitOfWork.Save();
                return _mapper.Map<ProgramDTO>(programEntity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

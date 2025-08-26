using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Domain.Concrete
{
    internal class StudentCardDomain : DomainBase, IStudentCardDomain
    {
        public StudentCardDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private IStudentCardRepository StudentCardRepository => _unitOfWork.GetRepository<IStudentCardRepository>();

        public void Add(StudentCardPostDTO dto)
        {
            var entity = _mapper.Map<TblStudentCard>(dto);
            entity.Id = Guid.NewGuid();
            StudentCardRepository.Add(entity);
            _unitOfWork.Save();
        }

        public void Delete(Guid id)
        {
            StudentCardRepository.Remove(id);
            _unitOfWork.Save();
        }

        public IEnumerable<StudentCardDTO> GetAll()
        {
            var items = StudentCardRepository.GetAll();
            return _mapper.Map<IEnumerable<StudentCardDTO>>(items);
        }

        public StudentCardDTO GetById(Guid id)
        {
            var entity = StudentCardRepository.GetById(id);
            if (entity == null) throw new Exception("Student card not found");
            return _mapper.Map<StudentCardDTO>(entity);
        }

        public void Update(Guid id, StudentCardPostDTO dto)
        {
            var entity = StudentCardRepository.GetById(id);
            if (entity == null) throw new Exception("Student card not found");
            if (dto.UserId.HasValue) entity.UserId = dto.UserId.Value;
            if (dto.DepartmentId.HasValue) entity.DepartmentId = dto.DepartmentId.Value;
            if (dto.AcademicYearId.HasValue) entity.AcademicYearId = dto.AcademicYearId.Value;
            if (!string.IsNullOrWhiteSpace(dto.StudentCardCode)) entity.StudentCardCode = dto.StudentCardCode;
            StudentCardRepository.SetModified(entity);
            _unitOfWork.Save();
        }
    }
}

using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO;
using Entities.Models;
using Helpers;
using Helpers.Pagination;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;

namespace Domain.Concrete
{
    internal class StudentCardDomain : DomainBase, IStudentCardDomain
    {
        public StudentCardDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private IStudentCardRepository StudentCardRepository => _unitOfWork.GetRepository<IStudentCardRepository>();

        public void AddNew(StudentCardPostDTO dto)
        {
            if (!dto.UserId.HasValue || !dto.ProgramId.HasValue || !dto.AcademicYearId.HasValue)
                throw new Exception("Missing required data");

            StudentCardRepository.DisableCardsByUser(dto.UserId.Value);
            var entity = _mapper.Map<TblStudentCard>(dto);
            entity.Id = Guid.NewGuid();
            entity.CreatedDate = DateTimeOffset.UtcNow;
            entity.Status = EntityStatus.Active;
            entity.StudentCardCode = GenerateDigitCode();
            StudentCardRepository.Add(entity);
            _unitOfWork.Save();
        }

        public void Delete(Guid id)
        {
            StudentCardRepository.Remove(id);
            _unitOfWork.Save();
        }

        public Pagination<StudentCardDTO> GetAll(QueryParameters queryParameters, Guid? userId, Guid? academicYearId, Guid? programId)
        {
            var cards = StudentCardRepository.GetStudentCards(queryParameters, userId, academicYearId, programId);
            var paginatedData = Pagination<StudentCardDTO>.ToPagedList(cards, _mapper.Map<List<StudentCardDTO>>);
            return paginatedData;
        }

        public StudentCardDTO GetById(Guid id)
        {
            var entity = StudentCardRepository.GetById(id);
            if (entity == null) throw new Exception("Student card not found");
            return _mapper.Map<StudentCardDTO>(entity);
        }

        public StudentCardDTO Update(Guid id, StudentCardPostDTO dto)
        {
            var entity = StudentCardRepository.GetById(id);
            if (entity == null) throw new Exception("Student card not found");
            if (dto.UserId.HasValue) entity.UserId = dto.UserId.Value;
            if (dto.ProgramId.HasValue) entity.ProgramId = dto.ProgramId.Value;
            if (dto.AcademicYearId.HasValue) entity.AcademicYearId = dto.AcademicYearId.Value;
            StudentCardRepository.SetModified(entity);
            _unitOfWork.Save();
            return _mapper.Map<StudentCardDTO>(entity);
        }

        public void UpdateStatus(Guid id, EntityStatus status)
        {
            var entity = StudentCardRepository.GetById(id);
            if (entity == null) throw new Exception("Student card not found");
            entity.Status = status;
            if (status == EntityStatus.Disabled)
                entity.DisabledDate = DateTimeOffset.UtcNow;
            StudentCardRepository.SetModified(entity);
            _unitOfWork.Save();
        }

        private static string GenerateDigitCode(int length = 8)
        {
            var random = new Random();
            var chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = (char)('0' + random.Next(0, 10));
            }
            return new string(chars);
        }
    }
}

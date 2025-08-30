using System;
using System.Collections.Generic;
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
    internal class TeacherDomain : DomainBase, ITeacherDomain
    {
        public TeacherDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private ITeacherRepository TeacherRepository => _unitOfWork.GetRepository<ITeacherRepository>();

        public TeacherDTO AddNew(TeacherPostDTO teacherPostDTO)
        {
            if (!teacherPostDTO.UserId.HasValue)
                throw new Exception("Missing required data");

            var entity = _mapper.Map<TblTeacher>(teacherPostDTO);
            entity.Id = Guid.NewGuid();
            TeacherRepository.Add(entity);
            _unitOfWork.Save();

            var created = TeacherRepository.GetById(entity.Id);
            return _mapper.Map<TeacherDTO>(created);
        }

        public void Delete(Guid id)
        {
            TeacherRepository.Remove(id);
            _unitOfWork.Save();
        }

        public Pagination<TeacherDTO> GetAllTeachers(QueryParameters queryParameters)
        {
            var teachers = TeacherRepository.GetTeachers(queryParameters);
            var paginatedData = Pagination<TeacherDTO>.ToPagedList(teachers, _mapper.Map<List<TeacherDTO>>);
            return paginatedData;
        }

        public TeacherDTO GetTeacherById(Guid id)
        {
            var entity = TeacherRepository.GetById(id);
            if (entity == null)
            {
                throw new Exception("Teacher not found");
            }
            return _mapper.Map<TeacherDTO>(entity);
        }

        public TeacherDTO Update(Guid id, TeacherPostDTO teacherPostDTO)
        {
            var entity = TeacherRepository.GetById(id);
            if (entity == null)
            {
                throw new Exception("Teacher not found");
            }
            if (teacherPostDTO.UserId.HasValue)
                entity.UserId = teacherPostDTO.UserId.Value;
            TeacherRepository.SetModified(entity);
            _unitOfWork.Save();

            var updated = TeacherRepository.GetById(id);
            return _mapper.Map<TeacherDTO>(updated);
        }
    }
}

using System;
using System.Collections.Generic;
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
    internal class TeacherDomain : DomainBase, ITeacherDomain
    {
        public TeacherDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private ITeacherRepository TeacherRepository => _unitOfWork.GetRepository<ITeacherRepository>();
        private IUserRepository UserRepository => _unitOfWork.GetRepository<IUserRepository>();

        public TeacherDTO AddNew(TeacherPostDTO teacherPostDTO)
        {
            if (!teacherPostDTO.UserId.HasValue)
                throw new Exception("Missing required data");

            var entity = _mapper.Map<TblTeacher>(teacherPostDTO);
            entity.Id = Guid.NewGuid();
            entity.Status = teacherPostDTO.Status ?? EntityStatus.Active;
            TeacherRepository.Add(entity);
            _unitOfWork.Save();

            var created = TeacherRepository.GetById(entity.Id);
            return _mapper.Map<TeacherDTO>(created);
        }

        public void Delete(Guid id)
        {
            var teacher = TeacherRepository.GetById(id);
            if (teacher == null)
            {
                throw new Exception("Teacher not found");
            }
            TeacherRepository.Remove(id);
            UserRepository.Remove(teacher.UserId);
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
            if (teacherPostDTO.Status.HasValue)
                entity.Status = teacherPostDTO.Status.Value;
            TeacherRepository.SetModified(entity);
            _unitOfWork.Save();

            var updated = TeacherRepository.GetById(id);
            return _mapper.Map<TeacherDTO>(updated);
        }
    }
}

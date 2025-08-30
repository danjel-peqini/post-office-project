using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO;
using DTO.UserDTO;
using DTO.UserTypeDTO;
using Entities.Models;
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
        private ICourseRepository CourseRepository => _unitOfWork.GetRepository<ICourseRepository>();

        public CourseDTO addCourse(CoursePostDTO course, Guid departmantId)
        {
            try
            {
                var mapped = _mapper.Map<TblCourse>(course);
                mapped.IsActive = true;
                mapped.DepartmentId = departmantId;
                mapped.Id = Guid.NewGuid();
                CourseRepository.Add(mapped);
                _unitOfWork.Save();
                return _mapper.Map<CourseDTO>(course);

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
                mapper.IsActive = true;
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

        public CourseDTO updateCourse(CoursePostDTO course, Guid courseId)
        {
            try
            {
                TblCourse courseEntity = CourseRepository.GetById(courseId);
                if (courseEntity == null)
                {
                    throw new Exception("Course not found");
                }
                if(course.TotalHours.HasValue)
                    courseEntity.TotalHours = course.TotalHours.Value;
                if(course.IsActive.HasValue)
                    courseEntity.IsActive = course.IsActive.Value;
                if (!string.IsNullOrWhiteSpace(course.Name))
                    courseEntity.Name = course.Name;
                if (course.Credits.HasValue)
                    courseEntity.Credits = course.Credits.Value;
                if(course.DepartmantId.HasValue)
                    courseEntity.DepartmentId = course.DepartmantId.Value;
                CourseRepository.SetModified(courseEntity);
                _unitOfWork.Save();
                return _mapper.Map<CourseDTO>(courseEntity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

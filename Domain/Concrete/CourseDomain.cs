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
    internal class CourseDomain : DomainBase, ICourseDomain
    {
        public CourseDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private ICourseRepository CourseRepository => _unitOfWork.GetRepository<ICourseRepository>();

        public void AddNew(CoursePostDTO course)
        {
            var entity = _mapper.Map<TblCourse>(course);
            entity.Id = Guid.NewGuid();
            entity.Status = course.Status ?? EntityStatus.Active;
            CourseRepository.Add(entity);
            _unitOfWork.Save();
        }

        public void Delete(Guid id)
        {
            CourseRepository.Remove(id);
            _unitOfWork.Save();
        }

        public Pagination<CourseDTO> GetAllCourses(QueryParameters queryParameters)
        {
            var courses = CourseRepository.GetCourses(queryParameters);
            var paginatedData = Pagination<CourseDTO>.ToPagedList(courses, _mapper.Map<List<CourseDTO>>);
            return paginatedData;
        }

        public CourseDTO GetCourseById(Guid id)
        {
            var entity = CourseRepository.GetById(id);
            if (entity == null)
            {
                throw new Exception("Course not found");
            }
            return _mapper.Map<CourseDTO>(entity);
        }

        public CourseDTO Update(Guid id, CoursePostDTO course)
        {
            var entity = CourseRepository.GetById(id);
            if (entity == null)
            {
                throw new Exception("Course not found");
            }
            if (!string.IsNullOrWhiteSpace(course.Name))
                entity.Name = course.Name;
            if (course.Credits.HasValue)
                entity.Credits = course.Credits.Value;
            if (course.TotalHours.HasValue)
                entity.TotalHours = course.TotalHours.Value;
            if (course.Status.HasValue)
                entity.Status = course.Status.Value;
            if (course.DepartmantId.HasValue)
                entity.DepartmentId = course.DepartmantId.Value;
            CourseRepository.SetModified(entity);
            _unitOfWork.Save();
            return _mapper.Map<CourseDTO>(entity);
        }
    }
}


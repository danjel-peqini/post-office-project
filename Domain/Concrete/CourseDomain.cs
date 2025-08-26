using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace Domain.Concrete
{
    internal class CourseDomain : DomainBase, ICourseDomain
    {
        public CourseDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private ICourseRepository CourseRepository => _unitOfWork.GetRepository<ICourseRepository>();
        private IGroupRepository GroupRepository => _unitOfWork.GetRepository<IGroupRepository>();
        private IGroupStudentRepository GroupStudentRepository => _unitOfWork.GetRepository<IGroupStudentRepository>();
        private IScheduleRepository ScheduleRepository => _unitOfWork.GetRepository<IScheduleRepository>();

        public CourseDTO Create(CourseCreateDTO dto)
        {
            var course = new TblCourse
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                DepartmentId = dto.DepartmentId,
                IsActive = true,
                Credits = dto.Credits,
                TotalHours = dto.TotalHours
            };
            CourseRepository.Add(course);

            var group = new TblGroup
            {
                Id = Guid.NewGuid(),
                Name = dto.GroupName,
                CourseId = course.Id,
                AcademicYearId = dto.AcademicYearId
            };
            GroupRepository.Add(group);

            foreach (var studentId in dto.StudentIds)
            {
                var gs = new TblGroupStudent
                {
                    Id = Guid.NewGuid(),
                    GroupId = group.Id,
                    StudentId = studentId
                };
                GroupStudentRepository.Add(gs);
            }

            foreach (var schedule in dto.Schedules)
            {
                var sch = new TblSchedule
                {
                    Id = Guid.NewGuid(),
                    GroupId = group.Id,
                    CourseId = course.Id,
                    TeacherId = schedule.TeacherId,
                    RoomId = schedule.RoomId,
                    DayOfWeek = schedule.DayOfWeek,
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime
                };
                ScheduleRepository.Add(sch);
            }

            _unitOfWork.Save();
            return _mapper.Map<CourseDTO>(course);
        }
    }
}

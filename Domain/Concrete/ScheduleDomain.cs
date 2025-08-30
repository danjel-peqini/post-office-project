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
    internal class ScheduleDomain : DomainBase, IScheduleDomain
    {
        public ScheduleDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private IScheduleRepository ScheduleRepository => _unitOfWork.GetRepository<IScheduleRepository>();

        public void AddNew(SchedulePostDTO schedule)
        {
            var entity = _mapper.Map<TblSchedule>(schedule);
            entity.Id = Guid.NewGuid();
            entity.Status = schedule.Status ?? EntityStatus.Active;
            ScheduleRepository.Add(entity);
            _unitOfWork.Save();
        }

        public void Delete(Guid id)
        {
            ScheduleRepository.Remove(id);
            _unitOfWork.Save();
        }

        public Pagination<ScheduleDTO> GetAllSchedules(QueryParameters queryParameters)
        {
            var schedules = ScheduleRepository.GetSchedules(queryParameters);
            var paginatedData = Pagination<ScheduleDTO>.ToPagedList(schedules, _mapper.Map<List<ScheduleDTO>>);
            return paginatedData;
        }

        public ScheduleDTO GetScheduleById(Guid id)
        {
            var entity = ScheduleRepository.GetById(id);
            if (entity == null)
            {
                throw new Exception("Schedule not found");
            }
            return _mapper.Map<ScheduleDTO>(entity);
        }

        public ScheduleDTO Update(Guid id, SchedulePostDTO schedule)
        {
            var entity = ScheduleRepository.GetById(id);
            if (entity == null)
            {
                throw new Exception("Schedule not found");
            }
            if (schedule.GroupId.HasValue)
                entity.GroupId = schedule.GroupId.Value;
            if (schedule.CourseId.HasValue)
                entity.CourseId = schedule.CourseId.Value;
            if (schedule.TeacherId.HasValue)
                entity.TeacherId = schedule.TeacherId.Value;
            if (schedule.RoomId.HasValue)
                entity.RoomId = schedule.RoomId.Value;
            if (schedule.AcademicYearId.HasValue)
                entity.AcademicYearId = schedule.AcademicYearId.Value;
            if (schedule.DayOfWeek.HasValue)
                entity.DayOfWeek = schedule.DayOfWeek.Value;
            if (schedule.StartTime.HasValue)
                entity.StartTime = schedule.StartTime.Value;
            if (schedule.EndTime.HasValue)
                entity.EndTime = schedule.EndTime.Value;
            if (schedule.ScheduleType.HasValue)
                entity.ScheduleType = schedule.ScheduleType.Value;
            if (schedule.Status.HasValue)
                entity.Status = schedule.Status.Value;
            ScheduleRepository.SetModified(entity);
            _unitOfWork.Save();
            return _mapper.Map<ScheduleDTO>(entity);
        }
    }
}

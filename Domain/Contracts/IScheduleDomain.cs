using System;
using DTO;
using Helpers.Pagination;

namespace Domain.Contracts
{
    public interface IScheduleDomain
    {
        Pagination<ScheduleDTO> GetAllSchedules(QueryParameters queryParameters, Guid? groupId, Guid? studentId, Guid? teacherId);
        ScheduleDTO GetScheduleById(Guid id);
        void AddNew(SchedulePostDTO schedule);
        ScheduleDTO Update(Guid id, SchedulePostDTO schedule);
        void Delete(Guid id);
    }
}

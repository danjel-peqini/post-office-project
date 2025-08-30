using DTO;
using Helpers.Pagination;

namespace Domain.Contracts
{
    public interface IScheduleDomain
    {
        Pagination<ScheduleDTO> GetAllSchedules(QueryParameters queryParameters);
        ScheduleDTO GetScheduleById(Guid id);
        void AddNew(SchedulePostDTO schedule);
        ScheduleDTO Update(Guid id, SchedulePostDTO schedule);
        void Delete(Guid id);
    }
}

using DTO;
using Helpers.Pagination;

namespace Domain.Contracts
{
    public interface IGroupDomain
    {
        Pagination<GroupDTO> GetAllGroups(QueryParameters queryParameters);
        GroupDTO GetGroupById(Guid id);
        void AddNew(GroupPostDTO group);
        GroupDTO Update(Guid id, GroupPostDTO group);
        void Delete(Guid id);
    }
}

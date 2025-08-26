using DTO;
using Helpers.Pagination;

namespace Domain.Contracts
{
    public interface IRoomDomain
    {
        Pagination<RoomDTO> GetAllRooms(QueryParameters queryParameters);
        RoomDTO GetRoomById(Guid id);
        void AddNew(RoomPostDTO roomPostDTO);
        RoomDTO Update(Guid id, RoomPostDTO roomPostDTO);
        void Delete(Guid id);
    }
}

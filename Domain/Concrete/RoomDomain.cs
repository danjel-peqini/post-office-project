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
    internal class RoomDomain : DomainBase, IRoomDomain
    {
        public RoomDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private IRoomRepository RoomRepository => _unitOfWork.GetRepository<IRoomRepository>();

        public void AddNew(RoomPostDTO roomPostDTO)
        {
            var entity = _mapper.Map<TblRoom>(roomPostDTO);
            entity.Id = Guid.NewGuid();
            RoomRepository.Add(entity);
            _unitOfWork.Save();
        }

        public void Delete(Guid id)
        {
            RoomRepository.Remove(id);
        }

        public Pagination<RoomDTO> GetAllRooms(QueryParameters queryParameters)
        {
            var rooms = RoomRepository.GetRooms(queryParameters);
            var paginatedData = Pagination<RoomDTO>.ToPagedList(rooms, _mapper.Map<List<RoomDTO>>);
            return paginatedData;
        }

        public RoomDTO GetRoomById(Guid id)
        {
            var entity = RoomRepository.GetById(id);
            if (entity == null)
            {
                throw new Exception("Room not found");
            }
            return _mapper.Map<RoomDTO>(entity);
        }

        public RoomDTO Update(Guid id, RoomPostDTO roomPostDTO)
        {
            var entity = RoomRepository.GetById(id);
            if (entity == null)
            {
                throw new Exception("Room not found");
            }
            if (!string.IsNullOrWhiteSpace(roomPostDTO.Name))
                entity.Name = roomPostDTO.Name;
            if (roomPostDTO.RoomType.HasValue)
                entity.RoomType = roomPostDTO.RoomType.Value;
            if (roomPostDTO.SeatsNumber.HasValue)
                entity.SeatsNumber = roomPostDTO.SeatsNumber.Value;
            RoomRepository.SetModified(entity);
            _unitOfWork.Save();
            return _mapper.Map<RoomDTO>(entity);
        }
    }
}

using Domain.Contracts;
using DTO;
using Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace PostOfficeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomDomain _roomDomain;

        public RoomController(IRoomDomain roomDomain)
        {
            _roomDomain = roomDomain;
        }

        [HttpPost]
        [Route("getAll")]
        public IActionResult GetAll([FromQuery] QueryParameters queryParameters)
            => Ok(_roomDomain.GetAllRooms(queryParameters));

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
            => Ok(_roomDomain.GetRoomById(id));

        [HttpPost]
        [Route("add")]
        public IActionResult AddNew([FromBody] RoomPostDTO dto)
        {
            _roomDomain.AddNew(dto);
            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] RoomPostDTO dto)
            => Ok(_roomDomain.Update(id, dto));

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _roomDomain.Delete(id);
            return Ok();
        }
    }
}

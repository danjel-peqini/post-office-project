using Domain.Contracts;
using DTO;
using Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace PostOfficeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupDomain _groupDomain;

        public GroupController(IGroupDomain groupDomain)
        {
            _groupDomain = groupDomain;
        }

        [HttpPost]
        [Route("getAll")]
        public IActionResult GetAll([FromQuery] QueryParameters queryParameters)
            => Ok(_groupDomain.GetAllGroups(queryParameters));

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
            => Ok(_groupDomain.GetGroupById(id));

        [HttpPost]
        [Route("add")]
        public IActionResult AddNew([FromBody] GroupPostDTO dto)
        {
            _groupDomain.AddNew(dto);
            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] GroupPostDTO dto)
            => Ok(_groupDomain.Update(id, dto));

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _groupDomain.Delete(id);
            return Ok();
        }
    }
}

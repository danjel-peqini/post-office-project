using Domain.Contracts;
using DTO.PostOfficeDTO;
using DTO.ShipmentDTO;
using Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace PostOfficeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostOfficeController : ControllerBase
    {
        private readonly IPostOfficeDomain _postOfficeDomain;

        public PostOfficeController(IPostOfficeDomain postOfficeDomain)
        {
            _postOfficeDomain = postOfficeDomain;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddNewPostOffice([FromBody] PostOfficePostDTO postOfficeDTO)
        {
            _postOfficeDomain.Add(postOfficeDTO);
            return Ok();
        }
        [HttpPost]
        [Route("getAll")]
        public IActionResult GetAll([FromQuery] QueryParameters queryParameters, [FromQuery] string? searchValue)
                => Ok(_postOfficeDomain.GetAll(queryParameters, searchValue));
        [HttpPut]
        [Route("updateStatus")]
        public IActionResult Update([FromBody] PostOfficeDTO postOfficeDTO)
        {
            _postOfficeDomain.Update(postOfficeDTO);
            return Ok();

        }
    }
}

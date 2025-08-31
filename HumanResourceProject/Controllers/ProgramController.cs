using Domain.Contracts;
using DTO;
using Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace PostOfficeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramDomain _programDomain;

        public ProgramController(IProgramDomain programDomain)
        {
            _programDomain = programDomain;
        }

        [HttpPost]
        [Route("getAll")]
        public IActionResult GetAll([FromQuery] QueryParameters queryParameters)
            => Ok(_programDomain.GetAllPrograms(queryParameters));

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
            => Ok(_programDomain.GetProgramById(id));

        [HttpPost]
        [Route("add")]
        public IActionResult AddNew([FromBody] ProgramPostDTO dto)
        {
            _programDomain.AddNew(dto);
            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] ProgramPostDTO dto)
            => Ok(_programDomain.Update(id, dto));

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _programDomain.Delete(id);
            return Ok();
        }
    }
}


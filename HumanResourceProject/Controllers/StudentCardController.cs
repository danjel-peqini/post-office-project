using Domain.Contracts;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PostOfficeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentCardController : ControllerBase
    {
        private readonly IStudentCardDomain _studentCardDomain;

        public StudentCardController(IStudentCardDomain studentCardDomain)
        {
            _studentCardDomain = studentCardDomain;
        }

        [HttpGet]
        public IActionResult GetAll()
            => Ok(_studentCardDomain.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
            => Ok(_studentCardDomain.GetById(id));

        [HttpPost]
        public IActionResult Add([FromBody] StudentCardPostDTO dto)
        {
            _studentCardDomain.Add(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] StudentCardPostDTO dto)
        {
            _studentCardDomain.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _studentCardDomain.Delete(id);
            return Ok();
        }
    }
}

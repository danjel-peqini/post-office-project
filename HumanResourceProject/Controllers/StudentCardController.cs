using Domain.Contracts;
using DTO;
using Helpers.Pagination;
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

        [HttpPost]
        [Route("getAll")]
        public IActionResult GetAll([FromQuery] QueryParameters queryParameters, [FromQuery] Guid? userId, [FromQuery] Guid? academicYearId, [FromQuery] Guid? programId)
            => Ok(_studentCardDomain.GetAll(queryParameters, userId, academicYearId, programId));

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
            => Ok(_studentCardDomain.GetById(id));

        [HttpPost]
        [Route("add")]
        public IActionResult AddNew([FromBody] StudentCardPostDTO dto)
        {
            _studentCardDomain.AddNew(dto);
            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] StudentCardPostDTO dto)
            => Ok(_studentCardDomain.Update(id, dto));

        [HttpPatch]
        [Route("{id}/status")]
        public IActionResult UpdateStatus([FromRoute] Guid id, [FromBody] StudentCardStatusUpdateDTO dto)
        {
            _studentCardDomain.UpdateStatus(id, dto.Status);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _studentCardDomain.Delete(id);
            return Ok();
        }
    }
}

using System;
using Domain.Contracts;
using DTO;
using Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace PostOfficeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AcademicYearController : ControllerBase
    {
        private readonly IAcademicYearDomain _academicYearDomain;

        public AcademicYearController(IAcademicYearDomain academicYearDomain)
        {
            _academicYearDomain = academicYearDomain;
        }

        [HttpPost]
        [Route("getAll")]
        public IActionResult GetAll([FromQuery] QueryParameters queryParameters)
            => Ok(_academicYearDomain.GetAllAcademicYears(queryParameters));

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
            => Ok(_academicYearDomain.GetAcademicYearById(id));

        [HttpPost]
        [Route("add")]
        public IActionResult AddNew([FromBody] AcademicYearPostDTO dto)
        {
            _academicYearDomain.AddNew(dto);
            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] AcademicYearPostDTO dto)
            => Ok(_academicYearDomain.Update(id, dto));
    }
}

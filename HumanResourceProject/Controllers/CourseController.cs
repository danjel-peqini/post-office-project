using Domain.Contracts;
using DTO;
using Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace PostOfficeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseDomain _courseDomain;

        public CourseController(ICourseDomain courseDomain)
        {
            _courseDomain = courseDomain;
        }

        [HttpPost]
        [Route("getAll")]
        public IActionResult GetAll([FromQuery] QueryParameters queryParameters)
            => Ok(_courseDomain.GetAllCourses(queryParameters));

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
            => Ok(_courseDomain.GetCourseById(id));

        [HttpPost]
        [Route("add")]
        public IActionResult AddNew([FromBody] CoursePostDTO dto)
        {
            _courseDomain.AddNew(dto);
            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] CoursePostDTO dto)
            => Ok(_courseDomain.Update(id, dto));

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _courseDomain.Delete(id);
            return Ok();
        }
    }
}


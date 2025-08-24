using Domain.Contracts;
using DTO;
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
        public IActionResult Create([FromBody] CourseCreateDTO dto)
        {
            var result = _courseDomain.Create(dto);
            return Ok(result);
        }
    }
}

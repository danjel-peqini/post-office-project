using Domain.Contracts;
using DTO;
using DTO.UserDTO;
using Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace PostOfficeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmantController : ControllerBase
    {
        private readonly IDepartmantDomain _departmantDomain;

      public DepartmantController(IDepartmantDomain departmantDomain)
        {
            _departmantDomain = departmantDomain;
        }
        [HttpPost]
        [Route("getAll")]
        public IActionResult GetAllUsers([FromQuery] QueryParameters queryParameters)
            => Ok(_departmantDomain.GetAllDepartmants(queryParameters));


        [HttpGet]
        [Route("{departmantId}")]
        public IActionResult GetUserById([FromRoute] Guid departmantId)
                   => Ok(_departmantDomain.GetDepartmantBtyd(departmantId));
        [HttpPost]
        [Route("add")]
        public IActionResult AddNew([FromBody] DepartmantPostDTO departmant)
        {
            _departmantDomain.AddNew(departmant);
            return Ok();
        }
        [HttpDelete]
        [Route("{departmantId}")]
        public IActionResult Delete([FromRoute] Guid departmantId)
        {
            _departmantDomain.Delete(departmantId);
            return Ok();
        }

        [HttpPatch]
        [Route("{departmantId}")]
        public IActionResult Update([FromRoute] Guid departmantId, [FromBody] DepartmantPostDTO departmant)
            => Ok(_departmantDomain.Update(departmantId, departmant));
        [HttpPost]
        [Route("{departmantId}/course")]
        public IActionResult addCourse([FromRoute] Guid departmantId, [FromBody] CoursePostDTO course)
        {
            _departmantDomain.addCourse(course, departmantId);
            return Ok();
        }
        [HttpPatch]
        [Route("course/{courseId}")]
        public IActionResult updateCourse([FromRoute] Guid courseId, [FromBody] CoursePostDTO course)
        {
            _departmantDomain.updateCourse(course, courseId);
            return Ok();
        }
    }
}

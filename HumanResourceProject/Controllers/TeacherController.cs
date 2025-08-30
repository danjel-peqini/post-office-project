using Domain.Contracts;
using DTO;
using Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace PostOfficeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherDomain _teacherDomain;

        public TeacherController(ITeacherDomain teacherDomain)
        {
            _teacherDomain = teacherDomain;
        }

        [HttpPost]
        [Route("getAll")]
        public IActionResult GetAll([FromQuery] QueryParameters queryParameters)
            => Ok(_teacherDomain.GetAllTeachers(queryParameters));

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
            => Ok(_teacherDomain.GetTeacherById(id));

        [HttpPost]
        [Route("add")]
        public IActionResult AddNew([FromBody] TeacherPostDTO dto)
        {
            var result = _teacherDomain.AddNew(dto);
            return Ok(result);
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] TeacherPostDTO dto)
            => Ok(_teacherDomain.Update(id, dto));

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _teacherDomain.Delete(id);
            return Ok();
        }

        [HttpDelete]
        [Route("user/{userId}")]
        public IActionResult DeleteByUserId([FromRoute] Guid userId)
        {
            _teacherDomain.DeleteByUserId(userId);
            return Ok();
        }
    }
}

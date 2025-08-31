using System;
using Domain.Contracts;
using DTO;
using Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace PostOfficeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleDomain _scheduleDomain;

        public ScheduleController(IScheduleDomain scheduleDomain)
        {
            _scheduleDomain = scheduleDomain;
        }

        [HttpPost]
        [Route("getAll")]
        public IActionResult GetAll([FromQuery] QueryParameters queryParameters, [FromQuery] Guid? groupId, [FromQuery] Guid? studentId, [FromQuery] Guid? teacherId)
            => Ok(_scheduleDomain.GetAllSchedules(queryParameters, groupId, studentId, teacherId));

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
            => Ok(_scheduleDomain.GetScheduleById(id));

        [HttpPost]
        [Route("add")]
        public IActionResult AddNew([FromBody] SchedulePostDTO dto)
        {
            _scheduleDomain.AddNew(dto);
            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] SchedulePostDTO dto)
            => Ok(_scheduleDomain.Update(id, dto));

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _scheduleDomain.Delete(id);
            return Ok();
        }
    }
}

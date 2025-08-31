using Domain.Contracts;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HumanResourceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceDomain _attendanceDomain;

        public AttendanceController(IAttendanceDomain attendanceDomain)
        {
            _attendanceDomain = attendanceDomain;
        }

        [HttpPost("checkin")]
        public IActionResult CheckIn([FromBody] AttendanceCheckInDTO dto)
        {
            var result = _attendanceDomain.CheckIn(dto);
            return Ok(result);
        }

        [HttpGet("student/{studentCardId}")]
        public IActionResult GetByStudent(Guid studentCardId)
        {
            var result = _attendanceDomain.GetByStudentCardId(studentCardId);
            return Ok(result);
        }

        [HttpGet("session/{sessionId}")]
        public IActionResult GetBySession(Guid sessionId)
        {
            var result = _attendanceDomain.GetBySessionId(sessionId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddAttendance([FromBody] AttendanceAddDTO dto)
        {
            var result = _attendanceDomain.AddAttendance(dto);
            return Ok(result);
        }

        [HttpDelete("{attendanceId}")]
        public IActionResult RemoveAttendance(Guid attendanceId)
        {
            _attendanceDomain.RemoveAttendance(attendanceId);
            return NoContent();
        }
    }
}

using Domain.Contracts;
using DTO;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using System.IO;
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

        [HttpPost("scan")]
        public IActionResult Scan([FromBody] AttendanceScanDTO dto)
        {
            var result = _attendanceDomain.Scan(dto);
            return Ok(result);
        }

        [HttpGet("student/{studentCardId}")]
        public IActionResult GetByStudent(Guid studentCardId)
        {
            var result = _attendanceDomain.GetByStudentCardId(studentCardId);
            return Ok(result);
        }

        [HttpGet("student/{studentCardId}/courses")]
        public IActionResult GetCourseAttendance(Guid studentCardId)
        {
            var result = _attendanceDomain.GetCourseAttendanceByStudent(studentCardId);
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

        [HttpGet("export")]
        public IActionResult ExportGroupSession([FromQuery] Guid groupId, [FromQuery] Guid sessionId)
        {
            var data = _attendanceDomain.GetGroupSessionAttendance(groupId, sessionId);
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Attendance");
            worksheet.Cell(1, 1).Value = "Student Card";
            worksheet.Cell(1, 2).Value = "First Name";
            worksheet.Cell(1, 3).Value = "Last Name";
            worksheet.Cell(1, 4).Value = "Has Attended";
            worksheet.Cell(1, 5).Value = "Check In Time";

            var row = 2;
            foreach (var item in data)
            {
                worksheet.Cell(row, 1).Value = item.StudentCardCode;
                worksheet.Cell(row, 2).Value = item.FirstName;
                worksheet.Cell(row, 3).Value = item.LastName;
                worksheet.Cell(row, 4).Value = item.HasAttended ? "Yes" : "No";
                worksheet.Cell(row, 5).Value = item.CheckInTime?.ToString("u");
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);
            var fileName = $"attendance_{groupId}_{sessionId}.xlsx";
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}

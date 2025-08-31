using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HumanResourceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbsenceWarningController : ControllerBase
    {
        private readonly IAbsenceWarningDomain _absenceWarningDomain;

        public AbsenceWarningController(IAbsenceWarningDomain absenceWarningDomain)
        {
            _absenceWarningDomain = absenceWarningDomain;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _absenceWarningDomain.GetAll();
            return Ok(result);
        }

        [HttpGet("student/{studentId}")]
        public IActionResult GetByStudent(Guid studentId)
        {
            var result = _absenceWarningDomain.GetByStudentId(studentId);
            return Ok(result);
        }
    }
}

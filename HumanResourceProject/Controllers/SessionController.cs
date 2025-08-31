using Domain.Contracts;
using Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PostOfficeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionDomain _sessionDomain;

        public SessionController(ISessionDomain sessionDomain)
        {
            _sessionDomain = sessionDomain;
        }

        [HttpPost]
        [Route("getAll")]
        public IActionResult GetAll([FromQuery] QueryParameters queryParameters, [FromQuery] Guid? teacherId)
            => Ok(_sessionDomain.GetAllSessions(queryParameters, teacherId));

        [HttpPost]
        [Route("{scheduleId}")]
        public IActionResult Create([FromRoute] Guid scheduleId)
            => Ok(_sessionDomain.CreateSession(scheduleId));

        [HttpPost]
        [Route("{sessionId}/regenerate-otp")]
        public IActionResult RegenerateOtp([FromRoute] Guid sessionId)
            => Ok(_sessionDomain.RegenerateOtp(sessionId));

        [HttpPost]
        [Route("{sessionId}/close")]
        public IActionResult Close([FromRoute] Guid sessionId)
            => Ok(_sessionDomain.CloseSession(sessionId));
    }
}

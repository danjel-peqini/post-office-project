using Domain.Contracts;
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

using Domain.Contracts;
using DTO.TransportDTO;
using DTO.UserDTO;
using Microsoft.AspNetCore.Mvc;

namespace PostOfficeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransportController : ControllerBase
    {
        private readonly ITransportDomain _transportDomain;

        public TransportController(ITransportDomain transportDomain)
        {
            _transportDomain = transportDomain;
        }

        [HttpGet]
        [Route("by-user")]
        public IActionResult GetUserById()
                  => Ok(_transportDomain.GetByUserId());
        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromQuery] TransportPostDTO transportPostDTO)
        {
            _transportDomain.AddNewTransport(transportPostDTO);
            return Ok();
        }
    }
}

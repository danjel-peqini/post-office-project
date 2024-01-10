using Domain.Contracts;
using DTO.UserDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserDomain UserDomain;

        public AuthController(IUserDomain userDomain)
        {
            UserDomain = userDomain;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromQuery] LoginUserDTO loginUserDTO)
            => Ok(UserDomain.Login(loginUserDTO));

        [HttpPost]
        [Route("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            UserDomain.Logout(string.Empty);
            return Ok();
        }

    }

}
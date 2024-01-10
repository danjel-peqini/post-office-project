using Domain.Contracts;
using DTO.UserDTO;
using Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceProject.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserDomain _userDomain;

        public UserController(IUserDomain userDomain)
        {
            _userDomain = userDomain;
        }


        [HttpPost]
        [Route("getAll")]
        public IActionResult GetAllUsers([FromQuery] QueryParameters queryParameters)
                    => Ok(_userDomain.GetAllUsers(queryParameters));


        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetUserById([FromRoute] Guid userId)
                   => Ok(_userDomain.GetUserById(userId));
        [HttpPost]
        [Route("add")]
        public IActionResult AddNewUser([FromQuery] UserPostDTO userPostDTO)
        {
            _userDomain.AddNewUser(userPostDTO);
            return Ok();
        }

            
    }
}


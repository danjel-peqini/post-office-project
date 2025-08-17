using Domain.Contracts;
using DTO.UserDTO;
using DTO.UserTypeDTO;
using Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace PostOfficeProject.Controllers
{
    

    [ApiController]
    [Route("[controller]")]
    public class UserTypeController: ControllerBase
        {
            private readonly IUserDomain _userDomain;

        public UserTypeController(IUserDomain userDomain)
        {
            _userDomain = userDomain;
        }


  

   
        [HttpPost]
        [Route("add")]
        public IActionResult AddNewUser([FromBody] UserTypePostDto userPostDTO)
        {
            _userDomain.AddNewUserType(userPostDTO);
            return Ok();
        }

     
        [HttpGet]
        [Route("user-type/all")]
        public IActionResult GetAllUserTypes()
        {
            return Ok(_userDomain.GetAllUserTypes());
        }
    }
}

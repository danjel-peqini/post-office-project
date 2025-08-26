using Domain.Contracts;
using DTO.UserDTO;
using Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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


        [HttpPost]
        [Route("by-user-types")]
        public IActionResult GetUsersByTypeIds([FromBody] IEnumerable<Guid> userTypeIds, [FromQuery] QueryParameters queryParameters)
                    => Ok(_userDomain.GetUsersByTypeIds(userTypeIds, queryParameters));


        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetUserById([FromRoute] Guid userId)
                   => Ok(_userDomain.GetUserById(userId));
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddNewUser([FromBody] UserPostDTO userPostDTO)
        {
            await _userDomain.AddNewUser(userPostDTO);
            return Ok();
        }

        [HttpPut]
        [Route("{userId}/password")]
        public IActionResult ChangePassword([FromRoute] Guid userId, [FromBody] ChangePasswordDTO changePasswordDTO)
        {
            _userDomain.ChangePassword(userId, changePasswordDTO);
            return Ok();
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDTO)
        {
            await _userDomain.ForgotPassword(forgotPasswordDTO);
            return Ok();
        }

        [HttpGet]
        [Route("user-type/{userTypeId}")]
        public IActionResult GetUserType([FromRoute] Guid userTypeId)
        {
            return Ok(_userDomain.GetUserTypeById(userTypeId));
        }
        [HttpGet]
        [Route("user-type/all")]
        public IActionResult GetAllUserTypes()
        {
            return Ok(_userDomain.GetAllUserTypes());
        }
    }

}


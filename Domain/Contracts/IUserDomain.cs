using DTO.UserDTO;
using Helpers.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUserDomain
    {
        Pagination<UserGetDTO> GetAllUsers(QueryParameters queryParameters);
        UserGetDTO GetUserById(Guid id);
        void AddNewUser(UserPostDTO userPostDTO);
        void UpdateUserStatus(Guid userId, UserPutDTO userPostDTO);
        string Login(LoginUserDTO loginUserDTO);
        void Logout(string token);
       
    }
}

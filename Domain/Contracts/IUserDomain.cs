using DTO.UserDTO;
using DTO.UserTypeDTO;
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
        Task AddNewUser(UserPostDTO userPostDTO);
        void AddNewUserType(UserTypePostDto userTipePostDto);
        void UpdateUserStatus(Guid userId, UserPutDTO userPostDTO);
        string Login(LoginUserDTO loginUserDTO);
        void Logout(string token);
        UserTypeDTO GetUserTypeById(Guid id);
        List<UserTypeDTO> GetAllUserTypes();
        void ChangePassword(Guid userId, ChangePasswordDTO changePasswordDTO);
        Task ForgotPassword(ForgotPasswordDTO forgotPasswordDTO);
        List<UserGetDTO> GetUsersByTypeIds(List<Guid> userTypeIds);

    }
}

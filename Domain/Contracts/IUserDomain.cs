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
        Task<UserGetDTO> AddNewUser(UserPostDTO userPostDTO);
        void AddNewUserType(UserTypePostDto userTipePostDto);
        void UpdateUserStatus(Guid userId, UserPutDTO userPostDTO);
        void PatchUpdateUser(Guid userId, UserPatchDTO userPatchDTO);
        void DeleteUser(Guid userId);
        string Login(LoginUserDTO loginUserDTO);
        void Logout(string token);
        UserTypeDTO GetUserTypeById(Guid id);
        List<UserTypeDTO> GetAllUserTypes();
        void ChangePassword(Guid userId, ChangePasswordDTO changePasswordDTO);
        Task ForgotPassword(ForgotPasswordDTO forgotPasswordDTO);
        Pagination<UserGetDTO> GetUsersByTypeIds(IEnumerable<Guid> userTypeIds, QueryParameters queryParameters);

    }
}

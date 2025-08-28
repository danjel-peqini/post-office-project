using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.UserDTO;
using DTO.UserTypeDTO;
using Entities.Models;
using Helpers;
using Helpers.Email;
using Helpers.Pagination;
using Helpers.PasswordManager;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace Domain.Concrete
{
    internal class UserDomain : DomainBase, IUserDomain
    {
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        public UserDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, EmailService emailService, IConfiguration configuration) : base(unitOfWork, mapper, httpContextAccessor)
        {
            _emailService = emailService;
            _configuration = configuration;
        }
        private IUserRepository UserRepository => _unitOfWork.GetRepository<IUserRepository>();
        private IUserTypeRepository UserTypeRepository => _unitOfWork.GetRepository<IUserTypeRepository>();
        public Pagination<UserGetDTO> GetAllUsers(QueryParameters queryParameters)
        {
            var users = UserRepository.GetAllUsers(queryParameters);
            users.Data.ForEach(x => { x.UserType = UserTypeRepository.GetById(x.UserTypeId); });
            var paginatedData = Pagination<UserGetDTO>.ToPagedList(users, _mapper.Map<List<UserGetDTO>>);
            return paginatedData;
        }

        public Pagination<UserGetDTO> GetUsersByTypeIds(IEnumerable<Guid> userTypeIds, QueryParameters queryParameters)
        {
            if (userTypeIds == null || !userTypeIds.Any())
            {
                return new Pagination<UserGetDTO>(new List<UserGetDTO>(), 0, queryParameters.CurrentPage, queryParameters.PageSize);
            }

            var users = UserRepository.GetUsersByTypeIds(userTypeIds, queryParameters);
            users.Data.ForEach(x => { x.UserType = UserTypeRepository.GetById(x.UserTypeId); });
            return Pagination<UserGetDTO>.ToPagedList(users, _mapper.Map<List<UserGetDTO>>);
        }

        public UserGetDTO GetUserById(Guid id)
        {
            TblUser user = UserRepository.GetById(id);
            user.UserType = UserTypeRepository.GetById(user.UserTypeId);
            return _mapper.Map<UserGetDTO>(user);
        }
        public UserTypeDTO GetUserTypeById(Guid id)
        {
            TblUserType userType = UserTypeRepository.GetById(id);
            return _mapper.Map<UserTypeDTO>(userType);

        }
        public async Task AddNewUser(UserPostDTO userPostDTO)
        {
            // check if username is unique
            var checkUser = UserRepository.CheckIfUsernamExist(userPostDTO.Username);
            if (checkUser == null)
            {
                var generatedPassword = PasswordManager.GenerateRandomPassword(10);
                var mapper = _mapper.Map<TblUser>(userPostDTO);
                mapper.Id = Guid.NewGuid();
                mapper.CreatedDate = DateTimeOffset.Now;
                mapper.Status = EntityStatus.Active;
                mapper.CreatedBy = GetUserId();
                mapper.LastModifiedBy = GetUserId();
                mapper.LastModifiedDate = DateTimeOffset.Now;
                mapper.Password = PasswordManager.HashPassword(generatedPassword);
                
                // Send the generated password to user's email
                try
                {

                await _emailService.SendEmail(
                    mapper.Email,
                    "Your Account Password",
                    $"Hello {mapper.FirstName} {mapper.LastName},\n\nYour account has been created.\nUsername: {mapper.Username}\nPassword: {generatedPassword}\n\nPlease log in and change your password.");
                UserRepository.Add(mapper);
                _unitOfWork.Save();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("This username exist!");
            }
        }


        public string Login(LoginUserDTO loginUserDTO)
        {
            var user = UserRepository.CheckIfUsernamExist(loginUserDTO.Username);
            if (user == null)
            {
                throw new Exception("Username doesn't exist");
            }

            var isPasswordValid = PasswordManager.VerifyPassword(loginUserDTO.Password, user.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid username or password");
            }

            var tokenValue = GenerateToken.ReturnToken(loginUserDTO.Username, user.Id, user.UserType.Name, _configuration);
            if (tokenValue == null)
            {
                throw new Exception("Token generation failed");
            }

            return tokenValue;
        }

        public void ChangePassword(Guid userId, ChangePasswordDTO changePasswordDTO)
        {
            var user = UserRepository.GetById(userId);
            if (user == null)
            {
                throw new Exception("User doesn't exist");
            }

            user.Password = PasswordManager.HashPassword(changePasswordDTO.NewPassword);
            user.LastModifiedDate = DateTimeOffset.Now;
            user.LastModifiedBy = GetUserId();

            UserRepository.Update(user);
            _unitOfWork.Save();
        }

        public async Task ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            var user = UserRepository.CheckIfUsernamExist(forgotPasswordDTO.Username);
            if (user == null)
            {
                throw new Exception("Username doesn't exist");
            }

            var newPassword = PasswordManager.GenerateRandomPassword(10);
            user.Password = PasswordManager.HashPassword(newPassword);
            user.LastModifiedDate = DateTimeOffset.Now;
            user.LastModifiedBy = user.Id;

            UserRepository.Update(user);
            _unitOfWork.Save();

            try
            {
                await _emailService.SendEmail(
                    user.Email,
                    "Password Reset",
                    $"Hello {user.FirstName} {user.LastName},<br/>Your new password is: {newPassword}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Logout(string token)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserStatus(Guid userId, UserPutDTO userPostDTO)
        {
            var currentUser = UserRepository.GetById(userId);
            if(currentUser == null)
                throw new Exception("This is user doesn't exist");

            var mapper = _mapper.Map<TblUser>(userPostDTO);
            mapper.Id = userId;
            UserRepository.Update(mapper);
            throw new NotImplementedException();
        }

        public void AddNewUserType(UserTypePostDto userTypePostDTO)
        {
            if (userTypePostDTO.Name != null)
            {
                var mapper = _mapper.Map<TblUserType>(userTypePostDTO);
                mapper.Id = Guid.NewGuid();
                mapper.IsActive = true;
                UserTypeRepository.Add(mapper);
                _unitOfWork.Save();
            }
            else
            {
                throw new Exception("Role name mus not be null!");
            }
        }
        public List<UserTypeDTO> GetAllUserTypes()
        {
            var userTypes = UserTypeRepository.GetAll();
            return _mapper.Map<List<UserTypeDTO>>(userTypes);
        }
    }
}

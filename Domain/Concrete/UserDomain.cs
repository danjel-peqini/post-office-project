using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.UserDTO;
using Entities.Models;
using Helpers.Pagination;
using Helpers.PasswordManager;
using Microsoft.AspNetCore.Http;

namespace Domain.Concrete
{
    internal class UserDomain : DomainBase, IUserDomain
    {
        public UserDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }
        private IUserRepository UserRepository => _unitOfWork.GetRepository<IUserRepository>();
        public Pagination<UserGetDTO> GetAllUsers(QueryParameters queryParameters)
        {
            var users = UserRepository.GetAllUsers(queryParameters);
            var paginatedData = Pagination<UserGetDTO>.ToPagedList(users, _mapper.Map<List<UserGetDTO>>);
            return paginatedData;
        }

        public UserGetDTO GetUserById(Guid id)
        {
            TblUser user = UserRepository.GetById(id);
            return _mapper.Map<UserGetDTO>(user);
        }
        public void AddNewUser(UserPostDTO userPostDTO)
        {
            // check if username is unique
            var checkUser = UserRepository.CheckIfUsernamExist(userPostDTO.Username);
            if (checkUser == null)
            {
                var mapper = _mapper.Map<TblUser>(userPostDTO);
                mapper.UserId = Guid.NewGuid();
                mapper.DateCreated = DateTimeOffset.Now;
                mapper.IsActive = true;
                mapper.CreatedBy = GetUserId();
                mapper.Password = PasswordManager.HashPassword(userPostDTO.Password);
                UserRepository.Add(mapper);
                _unitOfWork.Save();
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

            var tokenValue = GenerateToken.ReturnToken(loginUserDTO.Username, user.UserId, user.UserType.Name);
            if (tokenValue == null)
            {
                throw new Exception("Token generation failed");
            }

            return tokenValue;
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
            mapper.UserId = userId;
            UserRepository.Update(mapper);
            throw new NotImplementedException();
        }
    }
}

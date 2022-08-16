using FloraEducationAPI.Domain.DTO.Authentication;
using FloraEducationAPI.Domain.Models.Authentication;
using FloraEducationAPI.Repository.Interfaces;
using FloraEducationAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace FloraEducationAPI.Service.Implementations
{
    public class UserService : IUserService
    {
        public readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void CreateUser(User entity)
        {
            string passwordHash = BC.HashPassword(entity.Password);
            entity.Password = passwordHash;
            userRepository.Insert(entity);
        }

        public void DeleteUser(User entity)
        {
            userRepository.Delete(entity);
        }

        public List<User> FetchAllUsers()
        {
            return userRepository.FetchAllUsers().ToList();
        }

        public User FetchUserByEmail(string email)
        {
            return userRepository.FetchUserByEmail(email);
        }

        public User FetchUserByUsername(string username)
        {
            return userRepository.FetchUserByUsername(username);
        }

        public void UpdateUser(User entity)
        {
            userRepository.Update(entity);
        }

        public User Authenticate(UserLoginDTO userLoginDTO)
        {
            return userRepository.VerifyUserCredentials(userLoginDTO.Username, userLoginDTO.Password);
        }

        public User Register(UserRegisterDTO userRegisterDTO)
        {
            User user = new User
            {
                Email = userRegisterDTO.Email,
                Username = userRegisterDTO.Username,
                Password = BC.HashPassword(userRegisterDTO.Password),
                Role = "StandardUser",  // Every user by default is a standard user
                Name = userRegisterDTO.Name,
                Surname = userRegisterDTO.Surname
            };

            userRepository.Insert(user);
            return user;
        }

        public bool UserExists(UserRegisterDTO userRegisterDTO)
        {
            return userRepository.UserExists(userRegisterDTO.Username, userRegisterDTO.Email);
        }
    }
}

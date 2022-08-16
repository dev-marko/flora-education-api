using FloraEducationAPI.Domain.DTO.Authentication;
using FloraEducationAPI.Domain.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Service.Interfaces
{
    public interface IUserService
    {
        List<User> FetchAllUsers();
        User FetchUserByEmail(string email);
        User FetchUserByUsername(string username);
        User Authenticate(UserLoginDTO userLoginDTO);
        User Register(UserRegisterDTO userRegisterDTO);
        bool UserExists(UserRegisterDTO userRegisterDTO);
        void CreateUser(User entity);
        void UpdateUser(User entity);
        void DeleteUser(User entity);
    }
}

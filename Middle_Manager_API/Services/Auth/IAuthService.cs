using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Services.Auth
{
    public interface IAuthService
    {
        Task<bool> Register(UserForRegisterDto userForRegisterDto);
        Task<User> Login(UserForLoginDto userForLoginDto);
        Task<User> FindByEmail(string emailAddress);
        Task Logout();
        Task<bool> UpdatePassword(User user, string newPassword);
    }
}

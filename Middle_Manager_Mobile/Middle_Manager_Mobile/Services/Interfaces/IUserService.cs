using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_Mobile.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> DeleteUser(int userId);
        Task<IEnumerable<UserForDetailedDto>> GetUsers(UserRole role);
        Task<User> GetUser(int userId);
        Task<bool> UpdateUser(UserForDetailedDto userForUpdate);
        Task<bool> ForgotPassword(string emailAddress);
    }
}

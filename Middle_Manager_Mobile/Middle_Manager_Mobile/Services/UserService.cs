using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.Services.Interfaces;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_Mobile.Services
{
    public class UserService : BaseService, IUserService
    {
        public async Task<bool> DeleteUser(int userId)
        {
            return await MMApi.DeleteUser(userId);
        }
        public async Task<bool> UpdateUser(UserForDetailedDto userForUpdate)
        {
            return await MMApi.UpdateUser(userForUpdate);
        }
        public async Task<IEnumerable<UserForDetailedDto>> GetUsers(UserRole role = UserRole.All)
        {
            return await MMApi.GetUsers(role);
        }
        public async Task<User> GetUser(int userId)
        {
            return await MMApi.GetUser(userId);
        }

        public async Task<bool> ForgotPassword(string emailAddress)
        {
            return await MMApi.ForgotPassword(emailAddress);
        }
    }
}

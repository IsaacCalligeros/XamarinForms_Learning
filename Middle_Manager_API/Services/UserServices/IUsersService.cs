using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_API.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UserForDetailedDto>> GetUsers(int organisationId, UserRole role);
        Task<UserForDetailedDto> GetUser(int userId);
        Task<bool> UpdateUser(string userName, int userForUpdateId, UserForDetailedDto userForUpdate);
        Task<bool> DeleteUser(string userName, int currentUserId, int userToDeleteId);
        User FindByEmail(string emailAddress);
    }
}

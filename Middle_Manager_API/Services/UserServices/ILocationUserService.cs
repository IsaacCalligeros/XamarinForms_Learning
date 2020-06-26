using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Services
{
    public interface ILocationUserService
    {
        Task<IEnumerable<LocationUser>> GetLocationUsers(int locationId);
        Task<LocationUser> GetUser(int locationUserId);
        Task<bool> CreateLocationUser(string userName, LocationUser locationUser);
        Task<bool> UpdateLocationUser(string userName, LocationUser locationUser);
        Task<bool> DeleteLocationUser(string userName, int userId, int locationId);
    }
}

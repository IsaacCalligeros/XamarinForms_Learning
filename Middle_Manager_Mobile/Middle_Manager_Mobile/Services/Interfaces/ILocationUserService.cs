using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharedClassLibrary.Models;

namespace Middle_Manager_Mobile.Services.Interfaces
{
    public interface ILocationUserService
    {
        Task<bool> CreateLocationUser(LocationUser locationUser);
        Task<bool> DeleteLocationUser(int locationUserId, int userToDeleteId);
        Task<IEnumerable<LocationUser>> GetLocationUsers(int locationId);
        Task<LocationUser> GetLocationUser(int locationUserId);
        Task<bool> UpdateLocationUser(LocationUser locationUser);
    }
}

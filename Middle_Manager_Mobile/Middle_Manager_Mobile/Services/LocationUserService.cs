using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.Services.Interfaces;
using SharedClassLibrary.Models;

namespace Middle_Manager_Mobile.Services
{
    public class LocationUserService : BaseService, ILocationUserService
    {
        public async Task<bool> CreateLocationUser(LocationUser locationUser)
        {
            return await MMApi.CreateLocationUser(locationUser);
        }

        public async Task<bool> DeleteLocationUser(int locationId, int userToDeleteId)
        {
            return await MMApi.DeleteLocationUser(locationId, userToDeleteId);
        }

        public async Task<IEnumerable<LocationUser>> GetLocationUsers(int locationId)
        {
            return await MMApi.GetLocationUsers(locationId);
        }
        
        public async Task<LocationUser> GetLocationUser(int locationUserId)
        {
            return await MMApi.GetLocationUser(locationUserId);
        }

        public async Task<bool> UpdateLocationUser(LocationUser locationUser)
        {
            return await MMApi.UpdateLocationUser(locationUser);
        }
    }
}

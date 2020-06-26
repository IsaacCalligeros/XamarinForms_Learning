using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Middle_Manager_Mobile.Services.Interfaces;
using SharedClassLibrary.Models;

namespace Middle_Manager_Mobile.Services
{
    public class LocationService : BaseService, ILocationService
    {
        public async Task<bool> CreateLocation(Location location)
        {
            var res = await MMApi.CreateLocation(location);
            return res;
        }

        public async Task<bool> UpdateLocation(Location location)
        {
            var res = await MMApi.UpdateLocation(location);
            return res;
        }

        public async Task<bool> DeleteLocation(int locationId)
        {
            return await MMApi.DeleteLocation(locationId);
        }

        public async Task<IEnumerable<Location>> GetLocations()
        {
            return await MMApi.GetLocations();
        }
        public async Task<IEnumerable<Location>> GetOrganisationLocations()
        {
            return await MMApi.GetOrganisationLocations();
        }

        public async Task<Location> GetLocation(int locationId)
        {
            return await MMApi.GetLocation(locationId);
        }
    }
}

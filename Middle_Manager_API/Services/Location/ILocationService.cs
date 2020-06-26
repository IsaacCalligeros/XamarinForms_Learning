using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Services.Locations
{ 
    public interface ILocationsService
    {
        Task<bool> CreateLocation(string userName, Location location);

        Task<bool> DeleteLocation(string userName, int locationId);

        Task<Location> GetLocation(string userName, int locationId);
        Task<IEnumerable<Location>> GetLocations(string userName, int userId);
        Task<IEnumerable<Location>> GetOrganisationLocations(int organisationId);
        Task<bool> UpdateLocation(string userName, Location location);

    }
}

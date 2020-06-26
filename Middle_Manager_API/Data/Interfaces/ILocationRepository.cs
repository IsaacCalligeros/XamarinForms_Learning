using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Data.Interfaces
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<IEnumerable<Location>> GetLocations(int userId);
        Task<IEnumerable<Location>> GetOrganisationLocations(int userId);
        Task<Location> GetLocation(int locationId);
    }
}

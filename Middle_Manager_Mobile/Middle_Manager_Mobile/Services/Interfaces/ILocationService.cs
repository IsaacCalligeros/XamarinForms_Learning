using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharedClassLibrary.Models;

namespace Middle_Manager_Mobile.Services.Interfaces
{
    public interface ILocationService
    {
        Task<bool> CreateLocation(Location location);
        Task<bool> DeleteLocation(int locationId);
        Task<IEnumerable<Location>> GetLocations();
        Task<Location> GetLocation(int locationId);
    }
}

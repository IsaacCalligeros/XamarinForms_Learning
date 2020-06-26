using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Middle_Manager_API.Data.Interfaces;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Services.Locations
{
    public class LocationsService : ServiceBaseHelper, ILocationsService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;

        public LocationsService(IRepositoryWrapper repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<bool> CreateLocation(string userName, Location location)
        {
            _repo.Location.Create(location);

            if (await _repo.SaveAsync(userName))
            {
                return true;
            }

            throw new Exception("Error Creating the Location");
        }

        public async Task<bool> DeleteLocation(string userName, int locationId)
        {
            var location = await _repo.Location.GetLocation(locationId);

            _repo.Location.Delete(location);

            if (await _repo.SaveAsync(userName))
                return true;

            throw new Exception("Error deleting the location");
        }

        public async Task<Location> GetLocation(string userName, int locationId)
        {
            return await _repo.Location.GetLocation(locationId);
        }

        public async Task<IEnumerable<Location>> GetLocations(string userName, int userId)
        {
            return await _repo.Location.GetLocations(userId);
        }
        public async Task<IEnumerable<Location>> GetOrganisationLocations(int organisationId)
        {
            return await _repo.Location.GetOrganisationLocations(organisationId);
        }

        public async Task<bool> UpdateLocation(string userName, Location location)
        {
            var locationFromRepo = await _repo.Location.GetLocation(location.LocationId);

            _mapper.Map(location, locationFromRepo);

            if (await _repo.SaveAsync(userName))
                return true;
            return false;
        }
    }
}